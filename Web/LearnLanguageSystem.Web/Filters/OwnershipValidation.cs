namespace LearnLanguageSystem.Web.Filters
{
    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Services.Data.Contests;
    using LearnLanguageSystem.Web.Controllers;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class OwnershipValidation : ActionFilterAttribute
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IContestsService contestsService;

        public OwnershipValidation(UserManager<ApplicationUser> userManager, IContestsService contestsService)
        {
            this.userManager = userManager;
            this.contestsService = contestsService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = (BaseController)context.Controller;
            var contestId = context.ActionArguments["id"] as string;
            var creatorId = this.contestsService.GetCreatorId(contestId).GetAwaiter().GetResult();
            var user = this.userManager.GetUserAsync(controller.User).GetAwaiter().GetResult();

            if (user.Id != creatorId)
            {
                context.Result = controller.Forbid();
            }
        }
    }
}
