namespace LearnLanguageSystem.Web.Controllers
{
    using LearnLanguageSystem.Data;

    using Microsoft.AspNetCore.Mvc;

    public class ContestsController : Controller
    {
        private readonly ApplicationDbContext db;

        public ContestsController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            //var contest = this.db.Contests.To<ContestViewModel>().First();

            return this.View();
        }

        public IActionResult Create()
        {
            return this.View();
        }

        public IActionResult Join()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Join(string key)
        {
            return this.View();
        }

        public IActionResult MyContests()
        {
            return this.View();
        }
    }
}
