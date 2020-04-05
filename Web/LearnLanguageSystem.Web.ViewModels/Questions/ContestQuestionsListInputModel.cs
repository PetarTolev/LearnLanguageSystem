namespace LearnLanguageSystem.Web.ViewModels.Questions
{
    using System.ComponentModel.DataAnnotations;

    public class ContestQuestionsListInputModel
    {
        public string Id { get; set; }

        [MinLength(1)]
        [MaxLength(10)]
        public QuestionInputModel[] Questions { get; set; }
    }
}
