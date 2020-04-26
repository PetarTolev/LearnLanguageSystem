namespace LearnLanguageSystem.Web.ViewModels.Rooms
{
    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Services.Mapping;

    public class AnswerCheckViewModel : IMapFrom<Answer>
    {
        public string Id { get; set; }

        public bool IsRight { get; set; }
    }
}
