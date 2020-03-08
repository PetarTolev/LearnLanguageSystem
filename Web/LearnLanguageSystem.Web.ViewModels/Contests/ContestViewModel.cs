namespace LearnLanguageSystem.Web.ViewModels.Contests
{
    using System.Collections.Generic;

    using LearnLanguageSystem.Data.Models.Contest;
    using LearnLanguageSystem.Services.Mapping;
    using LearnLanguageSystem.Web.ViewModels.Questions;

    public class ContestViewModel : IMapFrom<Contest>
    {
        public IEnumerable<QuestionViewModel> Questions { get; set; }
    }
}
