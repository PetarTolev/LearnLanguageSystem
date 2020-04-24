namespace LearnLanguageSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ErrorsController : BaseController
    {
        public IActionResult NotFound(string message = "We can't seem to find the page you're looking for.")
        {
            return this.View((object)message);
        }

        public IActionResult Forbid(string message = "We are sorry, but you do not have access to this page or resource.")
        {
            return this.View((object)message);
        }

        public IActionResult BadRequest(string message = "The request could not be understood by the server due to malformed syntax.")
        {
            return this.View((object)message);
        }
    }
}
