namespace LearnLanguageSystem.Web.InputModels.Contests
{
    using System.Collections.Generic;

    using LearnLanguageSystem.Web.InputModels.Questions;

    public class ContestInputModel
    {
        public List<QuestionInputModel> Questions { get; set; }
    }
}
