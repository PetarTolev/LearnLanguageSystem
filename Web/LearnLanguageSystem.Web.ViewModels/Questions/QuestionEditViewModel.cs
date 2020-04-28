namespace LearnLanguageSystem.Web.ViewModels.Questions
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using LearnLanguageSystem.Data.Models;
    using LearnLanguageSystem.Services.Mapping;
    using LearnLanguageSystem.Web.ViewModels.Answers;

    public class QuestionEditViewModel : IMapFrom<Question>
    {
        public string Id { get; set; }

        [Required]
        [StringLength(ModelConstants.Questions.MaxContentLength, MinimumLength = ModelConstants.Questions.MinContentLength, ErrorMessage = ModelConstants.Questions.ContentLengthError)]
        public string Content { get; set; }

        public IList<AnswerViewModel> Answers { get; set; }
    }
}
