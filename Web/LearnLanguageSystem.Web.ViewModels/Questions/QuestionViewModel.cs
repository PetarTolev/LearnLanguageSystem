namespace LearnLanguageSystem.Web.ViewModels.Questions
{
    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Services.Mapping;

    public class QuestionViewModel : IMapFrom<Question>
    {
        public string Id { get; set; }

        public string Content { get; set; }
    }
}
