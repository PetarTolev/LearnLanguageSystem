namespace LearnLanguageSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ContactsController : BaseController
    {
        public IActionResult Index()
        {
            return this.NoContent();
        }
    }
}
