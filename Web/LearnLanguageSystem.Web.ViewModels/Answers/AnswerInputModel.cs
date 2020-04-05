namespace LearnLanguageSystem.Web.ViewModels.Answers
{
    using System.ComponentModel.DataAnnotations;

    public class AnswerInputModel
    {
        [Required]
        [StringLength(300, MinimumLength = 3)]
        public string Content { get; set; }

        public bool IsRight { get; set; }
    }
}
