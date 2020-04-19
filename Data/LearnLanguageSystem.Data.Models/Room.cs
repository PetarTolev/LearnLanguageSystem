namespace LearnLanguageSystem.Data.Models
{
    using System.Collections.Generic;

    public class Room
    {
        public Room()
        {
            this.UsersIn = new HashSet<ApplicationUser>();
        }

        public int Id { get; set; }

        public string ContestId { get; set; }

        public virtual Contest Contest { get; set; }

        public virtual ICollection<ApplicationUser> UsersIn { get; set; }
    }
}
