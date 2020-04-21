namespace LearnLanguageSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class RankingController : BaseController
    {
        public IActionResult Index()
        {
            return this.NoContent();
        }
    }
}
