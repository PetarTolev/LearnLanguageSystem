namespace LearnLanguageSystem.Web.Filters
{
    using LearnLanguageSystem.Common;
    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Services.Data.Contests;
    using LearnLanguageSystem.Services.Data.Questions;
    using LearnLanguageSystem.Web.Controllers;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class OwnershipValidation : ActionFilterAttribute
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IContestsService contestsService;
        private readonly IQuestionsService questionsService;

        public OwnershipValidation(
            UserManager<ApplicationUser> userManager,
            IContestsService contestsService,
            IQuestionsService questionsService)
        {
            this.userManager = userManager;
            this.contestsService = contestsService;
            this.questionsService = questionsService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = (BaseController)context.Controller;
            var contestId = context.ActionArguments["id"] as string;
            string creatorId = string.Empty;

            var controllerName = controller.ControllerContext.ActionDescriptor.ControllerName;
            if (controllerName == "Contests")
            {
                creatorId = this.contestsService.GetCreatorId(contestId);
            }
            else if (controllerName == "Questions")
            {
                creatorId = this.questionsService.GetCreatorId(contestId);
            }

            if (creatorId == null)
            {
                context.Result = controller.NotFound();
                return;
            }

            // todo: Refactor
            var user = this.userManager.GetUserAsync(controller.User).GetAwaiter().GetResult();
            var isAdmin = this.userManager.IsInRoleAsync(user, GlobalConstants.AdministratorRoleName).GetAwaiter().GetResult();

            if (!isAdmin && user.Id != creatorId)
            {
                context.Result = controller.Forbid();
            }
        }
    }
}
