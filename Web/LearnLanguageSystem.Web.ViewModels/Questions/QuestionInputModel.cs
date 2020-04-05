namespace LearnLanguageSystem.Web.ViewModels.Questions
{
    using System.ComponentModel.DataAnnotations;

    using LearnLanguageSystem.Web.ViewModels.Answers;

    public class QuestionInputModel
    {
        [Required]
        [StringLength(300, MinimumLength = 3)]
        public string Content { get; set; }

        [Required]
        public AnswerInputModel One { get; set; }

        [Required]
        public AnswerInputModel Two { get; set; }

        [Required]
        public AnswerInputModel Three { get; set; }

        [Required]
        public AnswerInputModel Four { get; set; }
    }
}
