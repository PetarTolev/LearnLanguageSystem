namespace LearnLanguageSystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Services.Data.ApplicationSettings;
    using LearnLanguageSystem.Services.Data.Rooms;
    using LearnLanguageSystem.Web.ViewModels.Rooms;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class RoomsController : BaseController
    {
        private readonly IRoomsService roomsService;
        private readonly IApplicationSettingsService applicationSettingsService;
        private readonly UserManager<ApplicationUser> userManager;

        public RoomsController(
            IRoomsService roomsService,
            IApplicationSettingsService applicationSettingsService,
            UserManager<ApplicationUser> userManager)
        {
            this.roomsService = roomsService;
            this.applicationSettingsService = applicationSettingsService;
            this.userManager = userManager;
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

        public async Task<IActionResult> Close(string contestId)
        {
            var roomId = await this.roomsService.CloseAsync(contestId);

            if (roomId == null)
            {
                return this.BadRequest();
            }

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
                this.TempData["Notification"] = $"You have already join room. First, exit it so you can join into this one. Do you want to leave it.";
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

            return this.RedirectToAction(nameof(this.Index), new { id = roomId });
        }

        public IActionResult Start()
        {
            throw new NotImplementedException();
        }
    }
}
