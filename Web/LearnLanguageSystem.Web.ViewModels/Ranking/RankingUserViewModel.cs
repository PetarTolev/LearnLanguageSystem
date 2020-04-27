namespace LearnLanguageSystem.Web.ViewModels.Ranking
{
    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Services.Mapping;

    public class RankingUserViewModel : IMapFrom<ApplicationUser>
    {
        public string UserName { get; set; }

        public string AvatarUrl { get; set; }

        public int PointsFromContests { get; set; }
    }
}
