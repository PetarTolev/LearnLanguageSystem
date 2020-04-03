namespace LearnLanguageSystem.Web.Controllers
{
    using System.Threading.Tasks;

    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Services.Data.Contests;
    using LearnLanguageSystem.Web.ViewModels.Contests;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class ContestsController : BaseController
    {
        private readonly IContestsService contestsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ContestsController(IContestsService contestsService, UserManager<ApplicationUser> userManager)
        {
            this.contestsService = contestsService;
            this.userManager = userManager;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContestNameInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var contestId = await this.contestsService.CreateAsync(model.Name, user.Id);

            return this.RedirectToAction(nameof(this.Edit), new { id = contestId });
        }

        public IActionResult Edit(string id)
        {
            var contest = this.contestsService.GetById<ContestViewModel>(id);

            return this.View(contest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeName(string id, ContestNameInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.Edit), new { id = id });
            }

            await this.contestsService.ChangeNameAsync(id, input.Name);

            return this.RedirectToAction(nameof(this.MyContests));
        }

        public IActionResult Delete(string id)
        {
            return this.RedirectToAction(nameof(this.MyContests));
        }

        public IActionResult Open(string id)
        {
            return this.RedirectToAction(nameof(this.Join));
        }

        public IActionResult Join()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Join(string key)
        {
            return this.View();
        }

        public async Task<IActionResult> MyContests()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var contests = this.contestsService.GetOwned<ContestViewModel>(user.Id);

            var model = new ContestsListViewModel
            {
                Contests = contests,
            };

            return this.View(model);
        }
    }
}
