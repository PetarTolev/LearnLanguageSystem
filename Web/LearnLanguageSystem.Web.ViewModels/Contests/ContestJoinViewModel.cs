namespace LearnLanguageSystem.Web.ViewModels.Contests
{
    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Services.Mapping;

    public class ContestJoinViewModel : IMapFrom<Contest>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public bool IsOpen { get; set; }

        public string AccessCode { get; set; }
    }
}
