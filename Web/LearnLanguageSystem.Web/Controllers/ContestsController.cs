namespace LearnLanguageSystem.Web.Controllers
{
    using System.Threading.Tasks;

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

        [IdExistValidation]
        [ServiceFilter(typeof(OwnershipValidation))]
        public IActionResult Details(string id)
        {
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
        public async Task<IActionResult> ChangeName(ChangeNameInputModel model)
        {
            var isSuccessfully = await this.contestsService.ChangeNameAsync(model.Id, model.Name);

            if (!isSuccessfully)
            {
                return this.RedirectToAction("BadRequest", "Errors");
            }

            return this.RedirectToAction(nameof(this.MyContests));
        }

        [IdExistValidation]
        [ServiceFilter(typeof(OwnershipValidation))]
        public async Task<IActionResult> Delete(string id)
        {
            if (this.roomsService.IsExistRoomWithThisContest(id))
            {
                return this.RedirectToAction("BadRequest", "Errors");
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
