namespace LearnLanguageSystem.Web.Controllers
{
    using System.Linq;

    using LearnLanguageSystem.Data;
    using LearnLanguageSystem.Web.ViewModels.Ranking;
    using Microsoft.AspNetCore.Mvc;

    public class RankingController : BaseController
    {
        private readonly ApplicationDbContext context;

        public RankingController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var model = this.context.Users
                .Select(x =>
                    new RankingUserViewModel
                    {
                        UserName = x.UserName,
                        AvatarUrl = x.AvatarUrl,
                        PointsFromContests = x.PointsFromContests,
                    })
                .OrderByDescending(x => x.PointsFromContests)
                .ThenBy(x => x.UserName)
                .ToList();

            return this.View(model);
        }
    }
}
