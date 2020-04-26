namespace LearnLanguageSystem.Web.ViewModels.Rooms
{
    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Services.Mapping;

    public class AnswerViewModel : IMapFrom<Answer>
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public string ChosenAnswer { get; set; }
    }
}
