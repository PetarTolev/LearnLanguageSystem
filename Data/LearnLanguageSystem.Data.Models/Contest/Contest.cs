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
            this.PlayersIn = new HashSet<ApplicationUser>();
            this.Questions = new HashSet<Question>();
        }

        public string Name { get; set; }

        public bool IsOpen { get; set; }

        public string AccessCode { get; set; }

        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public virtual ICollection<ApplicationUser> PlayersIn { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
