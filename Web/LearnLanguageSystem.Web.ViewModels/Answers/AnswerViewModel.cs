namespace LearnLanguageSystem.Web.ViewModels.Answers
{
    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Services.Mapping;

    public class AnswerViewModel : IMapFrom<Answer>
    {
        public string Content { get; set; }

        public bool IsRight { get; set; }
    }
}
