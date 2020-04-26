namespace LearnLanguageSystem.Web.ViewModels.Rooms
{
    using System.Collections.Generic;

    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Services.Mapping;

    public class QuestionCheckViewModel : IMapFrom<Question>
    {
        public string Id { get; set; }

        public ICollection<AnswerCheckViewModel> Answers { get; set; }
    }
}
