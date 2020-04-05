namespace LearnLanguageSystem.Web.ViewModels.Contests
{
    using System.Collections.Generic;

    using LearnLanguageSystem.Data.Models.Contest;
    using LearnLanguageSystem.Services.Mapping;
    using LearnLanguageSystem.Web.ViewModels.Questions;

    public class ContestViewModel : IMapFrom<Contest>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public bool IsOpen { get; set; }

        public ICollection<QuestionViewModel> Questions { get; set; }
    }
}
