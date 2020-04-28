namespace LearnLanguageSystem.Web.ViewModels.Answers
{
    using System.ComponentModel.DataAnnotations;

    public class AnswerInputModel
    {
        [Required]
        [StringLength(ModelConstants.Answers.MaxContentLength, MinimumLength = ModelConstants.Answers.MinContentLength, ErrorMessage = ModelConstants.Answers.ContentLengthError)]
        public string Content { get; set; }

        public bool IsRight { get; set; }
    }
}
