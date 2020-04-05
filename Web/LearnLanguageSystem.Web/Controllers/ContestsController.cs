namespace LearnLanguageSystem.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using LearnLanguageSystem.Data.Common.Repositories;
    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Data.Models.Contest;
    using LearnLanguageSystem.Services.Data.Contests;
    using LearnLanguageSystem.Web.Filters;
    using LearnLanguageSystem.Web.ViewModels.Contests;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Authorize]
    public class ContestsController : BaseController
    {
        private readonly IContestsService contestsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDeletableEntityRepository<Contest> contestRepository;

        public ContestsController(
            IContestsService contestsService,
            UserManager<ApplicationUser> userManager,
            IDeletableEntityRepository<Contest> contestRepository)
        {
            this.contestsService = contestsService;
            this.userManager = userManager;
            this.contestRepository = contestRepository;
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

            return this.RedirectToAction(nameof(this.Edit), new { id = contestId });
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return this.BadRequest();
            }

            var contest = await this.contestsService.GetByIdAsync<ContestViewModel>(id);

            return this.View(contest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ModelStateValidation]
        public async Task<IActionResult> Edit(ContestViewModel model)
        {
            await this.contestsService.ChangeNameAsync(model.Id, model.Name);

            return this.RedirectToAction(nameof(this.MyContests));
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var contest = await this.contestRepository
                .All()
                .Where(c => c.Id == id)
                .Include(c => c.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync();

            this.contestRepository.HardDelete(contest);
            await this.contestRepository.SaveChangesAsync();

            return this.RedirectToAction(nameof(this.MyContests));
        }

        public async Task<IActionResult> Open(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var code = await this.contestsService.OpenAsync(id);

            var model = new ContestOpenViewModel
            {
                Code = code,
            };

            this.TempData["IsLegit"] = true;
            return this.RedirectToAction(nameof(ContestsController.YourContestCode), model);
        }

        public IActionResult YourContestCode(ContestOpenViewModel model)
        {
            if (!this.TempData.TryGetValue("IsLegit", out _))
            {
                return this.BadRequest();
            }

            return this.View(model);
        }

        public IActionResult Join()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Join(string key)
        {
            await this.contestsService.GetByKeyAsync<Contest>(key);

            return this.View();
        }

        public async Task<IActionResult> MyContests()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var contests = await this.contestsService.GetOwnedAsync<ContestViewModel>(user.Id);

            var model = new ContestsListViewModel { Contests = contests };

            return this.View(model);
        }
    }
}
