namespace LearnLanguageSystem.Web.ViewModels.Rooms
{
    using System.Collections.Generic;

    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Services.Mapping;

    public class ContestCheckViewModel : IMapFrom<Contest>
    {
        public string Id { get; set; }

        public ICollection<QuestionCheckViewModel> Questions { get; set; }
    }
}
