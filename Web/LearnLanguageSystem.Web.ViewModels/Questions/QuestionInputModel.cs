namespace LearnLanguageSystem.Web.ViewModels.Questions
{
    using LearnLanguageSystem.Web.ViewModels.Answers;

    public class QuestionInputModel
    {
        public string Content { get; set; }

        public AnswerInputModel One { get; set; }

        public AnswerInputModel Two { get; set; }

        public AnswerInputModel Three { get; set; }

        public AnswerInputModel Four { get; set; }
    }
}
