namespace LearnLanguageSystem.Web.ViewModels.Contests
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Services.Mapping;
    using LearnLanguageSystem.Web.ViewModels.Questions;

    public class ContestViewModel : IMapFrom<Contest>
    {
        public string Id { get; set; }

        public string CreatorId { get; set; }

        public string Name { get; set; }

        public string RoomId { get; set; }

        public ICollection<QuestionViewModel> Questions { get; set; }
    }
}
