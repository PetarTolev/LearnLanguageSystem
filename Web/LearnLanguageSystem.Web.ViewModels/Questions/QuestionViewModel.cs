using LearnLanguageSystem.Data.Models.Contest;
using LearnLanguageSystem.Services.Mapping;

namespace LearnLanguageSystem.Web.ViewModels.Questions
{
    using System.Collections.Generic;

    using LearnLanguageSystem.Web.ViewModels.Answers;

    public class QuestionViewModel : IMapFrom<Question>
    {
        public string Content { get; set; }

        public IEnumerable<AnswerViewModel> Answers { get; set; }
    }
}
