namespace LearnLanguageSystem.Web.Controllers
{
    using System.Threading.Tasks;

    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Services.Data.ApplicationSettings;
    using LearnLanguageSystem.Services.Data.Contests;
    using LearnLanguageSystem.Web.Filters;
    using LearnLanguageSystem.Web.ViewModels.Contests;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class ContestsController : BaseController
    {
        private readonly IContestsService contestsService;
        private readonly IApplicationSettingsService applicationSettingsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ContestsController(
            IContestsService contestsService,
            IApplicationSettingsService applicationSettingsService,
            UserManager<ApplicationUser> userManager)
        {
            this.contestsService = contestsService;
            this.applicationSettingsService = applicationSettingsService;
            this.userManager = userManager;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ModelStateValidation]
        public async Task<IActionResult> Create(ContestNameInputModel model)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var contestId = await this.contestsService.CreateAsync(model.Name, user.Id);

            if (contestId == null)
            {
                return this.View();
            }

            return this.RedirectToAction(nameof(this.Edit), new { id = contestId });
        }

        [IdExistValidation]
        [ServiceFilter(typeof(OwnershipValidation))]
        public IActionResult Edit(string id)
        {
            var contest = this.contestsService.GetById<ContestViewModel>(id);

            if (contest == null)
            {
                return this.NotFound();
            }

            return this.View(contest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ModelStateValidation]
        public async Task<IActionResult> Edit(ContestViewModel model)
        {
            var contestId = await this.contestsService.ChangeNameAsync(model.Id, model.Name);

            if (contestId == null)
            {
                return this.NotFound();
            }

            return this.RedirectToAction(nameof(this.MyContests));
        }

        [IdExistValidation]
        [ServiceFilter(typeof(OwnershipValidation))]
        public async Task<IActionResult> Delete(string id)
        {
            var contestId = await this.contestsService.DeleteAsync(id);

            if (contestId == null)
            {
                return this.BadRequest();
            }

            return this.NoContent();
        }

        public async Task<IActionResult> MyContests()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var contests = this.contestsService.GetOwned<ContestViewModel>(user.Id);

            var model = new ContestsListViewModel { Contests = contests };

            return this.View(model);
        }

        [IdExistValidation]
        [ServiceFilter(typeof(OwnershipValidation))]
        public async Task<IActionResult> Open(string id)
        {
            var code = await this.contestsService.OpenAsync(id);

            if (code == null)
            {
                return this.BadRequest();
            }

            return this.RedirectToAction(nameof(ContestsController.Room), new { code });
        }

        public IActionResult Join()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Join(ContestJoinInputModel model)
        {
            var codeLength = this.applicationSettingsService.GetAccessCodeLength();

            if (model.Code.Length != codeLength)
            {
                this.TempData["Notification"] = $"Access code length must be {codeLength} numbers.";
                return this.View(model);
            }

            var contest = this.contestsService.GetByCode<ContestJoinViewModel>(model.Code);

            if (contest == null)
            {
                this.TempData["Notification"] = "Contest with this code doesn't exist.";
                return this.View(model);
            }

            return this.RedirectToAction(nameof(this.Room), new { code = contest.AccessCode });
        }

        public IActionResult Room(int code)
        {
            if (code == 0)
            {
                return this.NotFound();
            }

            return this.View((object)code);
        }

        [IdExistValidation]
        [ServiceFilter(typeof(OwnershipValidation))]
        public async Task<IActionResult> Close(string id)
        {
            var contestId = await this.contestsService.CloseAsync(id);

            if (contestId == null)
            {
                return this.BadRequest();
            }

            return this.RedirectToAction(nameof(this.MyContests));
        }

        public IActionResult Play(string id)
        {
            return this.View("Play", id);
        }
    }
}
