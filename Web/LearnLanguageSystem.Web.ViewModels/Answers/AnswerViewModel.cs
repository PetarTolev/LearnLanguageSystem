using LearnLanguageSystem.Data.Models.Contest;
using LearnLanguageSystem.Services.Mapping;

namespace LearnLanguageSystem.Web.ViewModels.Answers
{
    public class AnswerViewModel : IMapFrom<Answer>
    {
        public string Content { get; set; }

        public bool IsRight { get; set; }
    }
}
