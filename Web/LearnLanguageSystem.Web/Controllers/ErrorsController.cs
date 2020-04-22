namespace LearnLanguageSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ErrorsController : BaseController
    {
        public IActionResult NotFound(string message)
        {
            return this.View((object)message);
        }

        public IActionResult Forbid(string message)
        {
            return this.View((object)message);
        }

        public IActionResult BadRequest(string message)
        {
            return this.View((object)message);
        }
    }
}
