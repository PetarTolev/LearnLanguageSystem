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

        [Required]
        [StringLength(ModelConstants.Contests.MaxLength, MinimumLength = ModelConstants.Contests.MinLength, ErrorMessage = ModelConstants.NameLengthError)]
        public string Name { get; set; }

        public bool IsOpen { get; set; }

        public string AccessCode { get; set; }

        public ICollection<QuestionViewModel> Questions { get; set; }
    }
}
