namespace LearnLanguageSystem.Web.Components
{
    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Services.Data.Rooms;
    using LearnLanguageSystem.Web.ViewModels.Rooms;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class JoinedRoomComponent : ViewComponent
    {
        private readonly IRoomsService roomsService;
        private readonly UserManager<ApplicationUser> userManager;

        public JoinedRoomComponent(IRoomsService roomsService, UserManager<ApplicationUser> userManager)
        {
            this.roomsService = roomsService;
            this.userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var user = this.userManager.GetUserAsync(this.HttpContext.User).GetAwaiter().GetResult();

            var model = this.roomsService.GetById<JoinedRoomViewModel>(user.RoomId);

            return this.View(model);
        }
    }
}
