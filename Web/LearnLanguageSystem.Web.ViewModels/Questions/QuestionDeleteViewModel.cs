namespace LearnLanguageSystem.Web.ViewModels.Questions
{
    using LearnLanguageSystem.Data.Models.Contest;
    using LearnLanguageSystem.Services.Mapping;

    public class QuestionDeleteViewModel : IMapFrom<Question>
    {
        public string Id { get; set; }

        public string ContestId { get; set; }
    }
}
