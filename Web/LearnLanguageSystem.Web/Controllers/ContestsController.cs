namespace LearnLanguageSystem.Web.Controllers
{
    using System.Threading.Tasks;

    using LearnLanguageSystem.Common;
    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Services.Data.Contests;
    using LearnLanguageSystem.Services.Data.Rooms;
    using LearnLanguageSystem.Web.Filters;
    using LearnLanguageSystem.Web.ViewModels.Contests;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class ContestsController : BaseController
    {
        private readonly IContestsService contestsService;
        private readonly IRoomsService roomsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ContestsController(
            IContestsService contestsService,
            IRoomsService roomsService,
            UserManager<ApplicationUser> userManager)
        {
            this.contestsService = contestsService;
            this.roomsService = roomsService;
            this.userManager = userManager;
        }

        public IActionResult Create()
            => this.View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ModelStateValidation]
        public async Task<IActionResult> Create(ContestNameInputModel model)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var contestId = await this.contestsService.CreateAsync(model.Name, user.Id);

            if (contestId == null)
            {
                return this.RedirectToAction("BadRequest", "Errors");
            }

            return this.RedirectToAction(nameof(this.Details), new { id = contestId });
        }

        public async Task<IActionResult> Details(string id)
        {
            if (this.roomsService.IsExistRoomWithThisContest(id))
            {
                this.TempData["Notification"] = "You can’t make changes to the contest while it’s active.";
                return this.RedirectToAction(nameof(this.MyContests));
            }

            var creatorId = this.contestsService.GetCreatorId(id);
            var user = await this.userManager.GetUserAsync(this.User);
            var isAdmin = await this.userManager.IsInRoleAsync(user, GlobalConstants.AdministratorRoleName);

            if (!isAdmin && user.Id != creatorId)
            {
                return this.RedirectToAction("Forbid", "Errors");
            }

            var contest = this.contestsService.GetById<ContestViewModel>(id);

            if (contest == null)
            {
                return this.RedirectToAction("NotFound", "Errors", "Contest not exist!");
            }

            return this.View(contest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ModelStateValidation]
        public async Task<IActionResult> ChangeName(ContestViewModel model)
        {
            var isSuccessfully = await this.contestsService.ChangeNameAsync(model.Id, model.Name);

            if (!isSuccessfully)
            {
                return this.RedirectToAction("BadRequest", "Errors");
            }

            return this.RedirectToAction(nameof(this.MyContests));
        }

        public async Task<IActionResult> Delete(string id)
        {
            var creatorId = this.contestsService.GetCreatorId(id);
            var user = await this.userManager.GetUserAsync(this.User);
            var isAdmin = await this.userManager.IsInRoleAsync(user, GlobalConstants.AdministratorRoleName);

            if (!isAdmin && user.Id != creatorId)
            {
                return this.RedirectToAction("Forbid", "Errors");
            }

            if (this.roomsService.IsExistRoomWithThisContest(id))
            {
                this.TempData["Notification"] = "You can’t delete contest while it’s active.";
                return this.RedirectToAction(nameof(this.MyContests));
            }

            var isSuccessfully = await this.contestsService.DeleteAsync(id);

            if (!isSuccessfully)
            {
                return this.RedirectToAction("BadRequest", "Errors", new { Message = "Contest was not deleted." });
            }

            return this.NoContent();
        }

        public async Task<IActionResult> MyContests()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var model = this.contestsService.GetOwned<ContestViewModel>(user.Id);

            return this.View(model);
        }
    }
}
