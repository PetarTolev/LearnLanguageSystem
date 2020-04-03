namespace LearnLanguageSystem.Web.ViewModels.Questions
{
    using System.Collections.Generic;

    using LearnLanguageSystem.Data.Models.Contest;
    using LearnLanguageSystem.Services.Mapping;
    using LearnLanguageSystem.Web.ViewModels.Answers;

    public class QuestionViewModel : IMapFrom<Question>
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public AnswerViewModel One { get; set; }

        public AnswerViewModel Two { get; set; }

        public AnswerViewModel Three { get; set; }

        public AnswerViewModel Four { get; set; }
    }
}
