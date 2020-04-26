namespace LearnLanguageSystem.Web.ViewModels.Rooms
{
    using System.Collections.Generic;

    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Services.Mapping;

    public class QuestionViewModel : IMapFrom<Question>
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public List<AnswerViewModel> Answers { get; set; }
    }
}
