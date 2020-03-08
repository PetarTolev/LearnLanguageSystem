// ReSharper disable VirtualMemberCallInConstructor
namespace LearnLanguageSystem.Data.Models.Contest
{
    using System;
    using System.Collections.Generic;

    using LearnLanguageSystem.Data.Common.Models;

    public class Question : BaseDeletableModel<string>
    {
        public Question()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Answers = new HashSet<Answer>();
        }

        public string ContestId { get; set; }

        public string Content { get; set; }

        public virtual Contest Contest { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
    }
}
