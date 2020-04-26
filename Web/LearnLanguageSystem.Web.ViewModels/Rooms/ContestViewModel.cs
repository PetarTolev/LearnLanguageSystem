namespace LearnLanguageSystem.Web.ViewModels.Rooms
{
    using System.Collections.Generic;

    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Services.Mapping;

    public class ContestViewModel : IMapFrom<Contest>
    {
        public string Name { get; set; }

        public List<QuestionViewModel> Questions { get; set; }
    }
}
