namespace LearnLanguageSystem.Web.Filters
{
    using LearnLanguageSystem.Web.Controllers;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class IdExistValidation : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // todo: refactor chack by passing parameter from outside
            if (!context.ActionArguments.TryGetValue("id", out _))
            {
                var controller = (BaseController)context.Controller;
                context.Result = controller.BadRequest();
            }
        }
    }
}
