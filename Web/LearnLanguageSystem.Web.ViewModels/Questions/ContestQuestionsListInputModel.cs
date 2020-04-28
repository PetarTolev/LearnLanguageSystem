namespace LearnLanguageSystem.Web.ViewModels.Questions
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ContestQuestionsListInputModel
    {
        [Required]
        public string Id { get; set; }

        public IList<QuestionInputModel> Questions { get; set; }
    }
}
