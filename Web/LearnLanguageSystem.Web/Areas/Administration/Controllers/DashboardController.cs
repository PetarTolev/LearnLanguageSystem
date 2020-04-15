namespace LearnLanguageSystem.Web.Areas.Administration.Controllers
{
    using System.Linq;

    using LearnLanguageSystem.Data.Common.Repositories;
    using LearnLanguageSystem.Data.Models.Contest;
    using LearnLanguageSystem.Services.Mapping;
    using LearnLanguageSystem.Web.ViewModels.Administration.Dashboard;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdministrationController
    {
        private readonly IRepository<Contest> contestsRepository;

        public DashboardController(IRepository<Contest> contestsRepository)
        {
            this.contestsRepository = contestsRepository;
        }

        public IActionResult Index()
        {
            var contests = this.contestsRepository
                .AllAsNoTracking()
                .To<ContestViewModel>()
                .ToList();

            var model = new AllContestsViewModel
            {
                Contests = contests,
            };

            return this.View(model);
        }
    }
}
