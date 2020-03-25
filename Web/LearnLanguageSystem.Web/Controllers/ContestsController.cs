namespace LearnLanguageSystem.Web.Controllers
{
    using System.Collections.Generic;

    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Services.Data.Contests;
    using LearnLanguageSystem.Web.InputModels.Contests;
    using LearnLanguageSystem.Web.InputModels.Questions;
    using LearnLanguageSystem.Web.ViewModels.Contests;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ContestsController : Controller
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
        public IActionResult Create(ContestInputModel model)
        {
            return this.View();
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
        public IActionResult MyContests()
        {
            var userId = this.userManager.GetUserId(this.User);

            var model = this.contestsService.GetCreatedContests<ContestViewModel>(userId);

            return this.View();
        }
    }
}
