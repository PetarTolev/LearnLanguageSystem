namespace LearnLanguageSystem.Data.Models.Contest
{
    using System;

    using LearnLanguageSystem.Data.Common.Models;

    public class Answer : BaseDeletableModel<string>
    {
        public Answer()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Content { get; set; }

        public bool IsRight { get; set; }

        public string QuestionId { get; set; }

        public virtual Question Question { get; set; }
    }
}
