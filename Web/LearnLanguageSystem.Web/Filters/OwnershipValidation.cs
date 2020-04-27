namespace LearnLanguageSystem.Web.Filters
{
    using LearnLanguageSystem.Common;
    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Services.Data.Contests;
    using LearnLanguageSystem.Web.Controllers;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class OwnershipValidation : ActionFilterAttribute
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IContestsService contestsService;

        public OwnershipValidation(
            UserManager<ApplicationUser> userManager,
            IContestsService contestsService)
        {
            this.userManager = userManager;
            this.contestsService = contestsService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = (BaseController)context.Controller;
            var contestId = context.ActionArguments["id"] as string;
            var creatorId = this.contestsService.GetCreatorId(contestId);

            if (creatorId == null)
            {
                context.Result = controller.RedirectToAction("NotFound", "Errors", new { Message = "Contest not exist." });
                return;
            }

            var user = this.userManager.GetUserAsync(controller.User).GetAwaiter().GetResult();
            var isAdmin = this.userManager.IsInRoleAsync(user, GlobalConstants.AdministratorRoleName).GetAwaiter().GetResult();

            if (!isAdmin && user.Id != creatorId)
            {
                context.Result = controller.RedirectToAction("Forbid", "Errors", new { Message = "You have not created this contest. You are not allowed to make changes." });
            }
        }
    }
}
