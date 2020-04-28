namespace LearnLanguageSystem.Web.ViewModels.Answers
{
    using System.ComponentModel.DataAnnotations;

    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Services.Mapping;

    public class AnswerViewModel : IMapFrom<Answer>
    {
        [Required]
        [StringLength(ModelConstants.Answers.MaxContentLength, MinimumLength = ModelConstants.Answers.MinContentLength, ErrorMessage = ModelConstants.Answers.ContentLengthError)]
        public string Content { get; set; }

        public bool IsRight { get; set; }
    }
}
