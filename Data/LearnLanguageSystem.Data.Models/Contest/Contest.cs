// ReSharper disable VirtualMemberCallInConstructor
namespace LearnLanguageSystem.Data.Models.Contest
{
    using System;
    using System.Collections.Generic;

    using LearnLanguageSystem.Data.Common.Models;

    public class Contest : BaseDeletableModel<string>
    {
        public Contest()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Questions = new HashSet<Question>();
        }

        public ApplicationUser Creator { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
