namespace LearnLanguageSystem.Web.Controllers
{
    using System.Threading.Tasks;

    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Services.Data.Contests;
    using LearnLanguageSystem.Web.ViewModels.Contests;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ContestsController : BaseController
    {
        private readonly IContestsService contestsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ContestsController(IContestsService contestsService, UserManager<ApplicationUser> userManager)
        {
            this.contestsService = contestsService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(string name)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var contestId = await this.contestsService.CreateAsync(name, user.Id);

            return this.RedirectToAction(nameof(this.Edit), new { contestId = contestId });
        }

        [Authorize]
        public IActionResult Edit(string contestId)
        {
            var contest = this.contestsService.GetById<ContestViewModel>(contestId);

            return this.View(contest);
        }

        [Authorize]
        public IActionResult Join()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Join(string key)
        {
            return this.View();
        }

        [Authorize]
        public async Task<IActionResult> MyContests()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var model = this.contestsService.GetOwned<ContestViewModel>(user.Id);

            return this.View(model);
        }

        public async Task<IActionResult> ChangeName(string id, string name)
        {
            await this.contestsService.ChangeNameAsync(id, name);

            return this.RedirectToAction(nameof(this.MyContests)); // todo: ajax
        }
    }
}
