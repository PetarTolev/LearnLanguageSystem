namespace LearnLanguageSystem.Web.ViewModels.Questions
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ContestQuestionsListInputModel
    {
        [Required]
        public string Id { get; set; }

        [MinLength(1)]
        [MaxLength(10)]
        public IList<QuestionInputModel> Questions { get; set; }
    }
}
