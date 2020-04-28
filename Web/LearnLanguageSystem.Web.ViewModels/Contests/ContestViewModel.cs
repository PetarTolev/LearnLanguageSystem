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

        [Required(ErrorMessage = ModelConstants.RequiredError)]
        [StringLength(ModelConstants.MaxNameLength, MinimumLength = ModelConstants.MinNameLength, ErrorMessage = ModelConstants.NameLengthError)]
        public string Name { get; set; }

        [Required(ErrorMessage = ModelConstants.RequiredError)]
        [Range(ModelConstants.Contests.MinQuestionsCount, ModelConstants.Contests.MaxQuestionsCount, ErrorMessage = ModelConstants.Contests.QuestionsCountError)]
        public int QuestionCount { get; set; }

        public string RoomId { get; set; }

        public ICollection<QuestionViewModel> Questions { get; set; }
    }
}
