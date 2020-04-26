﻿namespace LearnLanguageSystem.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Services.Data.ApplicationSettings;
    using LearnLanguageSystem.Services.Data.Rooms;
    using LearnLanguageSystem.Web.Hubs;
    using LearnLanguageSystem.Web.ViewModels.Rooms;
    using LearnLanguageSystem.Web.ViewModels.Users;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SignalR;

    [Authorize]
    public class RoomsController : BaseController
    {
        private readonly IRoomsService roomsService;
        private readonly IApplicationSettingsService applicationSettingsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHubContext<RoomHub> roomHub;

        public RoomsController(
            IRoomsService roomsService,
            IApplicationSettingsService applicationSettingsService,
            UserManager<ApplicationUser> userManager,
            IHubContext<RoomHub> roomHub)
        {
            this.roomsService = roomsService;
            this.applicationSettingsService = applicationSettingsService;
            this.userManager = userManager;
            this.roomHub = roomHub;
        }

        public IActionResult All()
        {
            var rooms = this.roomsService.GetAll<AllRoomsListViewModel>();

            // todo: add current room if exist
            return this.View(rooms);
        }

        public async Task<IActionResult> Index(string id)
        {
            var room = this.roomsService.GetById<RoomIndexViewModel>(id);

            if (room == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            var user = await this.userManager.GetUserAsync(this.User);

            if (room.Users.Any(x => x.Id == user.Id))
            {
                return this.View(room);
            }

            return this.RedirectToAction(nameof(this.Join));
        }

        public async Task<IActionResult> Open(string contestId)
        {
            if (this.roomsService.IsExistRoomWithThisContest(contestId))
            {
                return this.BadRequest();
            }

            var roomId = await this.roomsService.OpenAsync(contestId);

            if (roomId == null)
            {
                return this.BadRequest();
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var isSuccessfullyAdded = await this.roomsService.AddUserAsync(roomId, user);

            if (!isSuccessfullyAdded)
            {
                return this.BadRequest();
            }

            return this.RedirectToAction(nameof(RoomsController.Index), new { id = roomId });
        }

        public async Task<IActionResult> Close(string roomId)
        {
            var usersIn = this.roomsService.GetUsersInIds(roomId).ToList();
            var currentUser = await this.userManager.GetUserAsync(this.User);
            usersIn.Remove(currentUser.Id);

            var isDeleted = await this.roomsService.CloseAsync(roomId);

            if (!isDeleted)
            {
                return this.BadRequest();
            }

            await this.roomHub.Clients.Users(usersIn).SendAsync("RedirectUser", "/");

            return this.RedirectToAction("MyContests", "Contests");
        }

        public IActionResult Join()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Join(RoomJoinInputModel model)
        {
            var codeLength = this.applicationSettingsService.GetAccessCodeLength();

            if (model.AccessCode.ToString().Length != codeLength)
            {
                // todo: extract in attribute
                this.ModelState.AddModelError("AccessCode", $"Access code length must be {codeLength} numbers.");
                return this.View(model);
            }

            var room = this.roomsService.GetByCode(model.AccessCode);

            if (room == null)
            {
                this.TempData["Notification"] = "Contest with this code doesn't exist.";
                return this.View(model);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            if (user.RoomId != null)
            {
                this.TempData["Notification"] = "You have already join room. First, exit it so you can join into this one. Do you want to leave it.";
                this.TempData["RoomToLeave"] = user.RoomId;
                return this.View(model);
            }

            if (!room.Users.Contains(user))
            {
                var isSuccessfullyAdded = await this.roomsService.AddUserAsync(room.Id, user);

                if (!isSuccessfullyAdded)
                {
                    return this.BadRequest();
                }
            }

            var userModel = new AddUserToRoomViewModel
            {
                AvatarUrl = user.AvatarUrl,
                Username = user.UserName,
                Id = user.Id,
                RoomId = room.Id,
                IsForOwner = false,
            };

            var usersIn = this.roomsService.GetUsersInIds(room.Id).ToList();
            usersIn.Remove(room.Contest.CreatorId);

            await this.roomHub.Clients.Users(usersIn).SendAsync("AddUserToRoom", userModel);

            userModel.IsForOwner = true;
            await this.roomHub.Clients.User(room.Contest.CreatorId).SendAsync("AddUserToRoom", userModel);

            return this.RedirectToAction(nameof(this.Index), new { id = room.Id });
        }

        public async Task<IActionResult> Kick(string roomId, string userId)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            var ownerId = this.roomsService.GetOwnerId(roomId);

            if (currentUser.Id != userId && ownerId != currentUser.Id)
            {
                return this.Forbid();
            }

            var userForKick = await this.userManager.FindByIdAsync(userId);

            var isSuccessfullyRemoved = await this.roomsService.RemoveUserAsync(roomId, userForKick);

            if (!isSuccessfullyRemoved)
            {
                return this.BadRequest();
            }

            var usersIn = this.roomsService.GetUsersInIds(roomId).ToList();

            await this.roomHub.Clients.User(userId).SendAsync("RedirectUser", "/Rooms/Join");
            await this.roomHub.Clients.Users(usersIn).SendAsync("RemoveUserFromRoom", userId);

            return this.NoContent();
        }

        public IActionResult Start(string roomId)
        {
            var usersIn = this.roomsService
                .GetUsersInIds(roomId)
                .ToList();

            this.roomHub.Clients.Users(usersIn).SendAsync("StartContest");
            this.roomHub.Clients.Users(usersIn).SendAsync("RedirectUser", $"/Rooms/Play?roomId={roomId}");

            return this.NoContent();
        }

        public IActionResult Play(string roomId)
        {
            var room = this.roomsService.GetById<RoomPlayViewModel>(roomId);

            return this.View(room);
        }

        public IActionResult Send(RoomPlayViewModel model)
        {
            return this.NoContent();
        }
    }
}
