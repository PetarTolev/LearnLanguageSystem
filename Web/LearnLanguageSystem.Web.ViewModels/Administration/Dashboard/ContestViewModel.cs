namespace LearnLanguageSystem.Web.ViewModels.Administration.Dashboard
{
    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Services.Mapping;

    public class ContestViewModel : IMapFrom<Contest>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string CreatorUserName { get; set; }

        public int QuestionsCount { get; set; }

        public bool IsOpen { get; set; }
    }
}
