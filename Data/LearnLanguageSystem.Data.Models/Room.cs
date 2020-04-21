namespace LearnLanguageSystem.Data.Models
{
    using System;
    using System.Collections.Generic;

    using LearnLanguageSystem.Data.Common.Models;

    public class Room : BaseModel<string>
    {
        public Room()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Users = new HashSet<ApplicationUser>();
        }

        public int AccessCode { get; set; }

        public string ContestId { get; set; }

        public virtual Contest Contest { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
