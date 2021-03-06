﻿// ReSharper disable VirtualMemberCallInConstructor
namespace LearnLanguageSystem.Data.Models
{
    using System;
    using System.Collections.Generic;

    using LearnLanguageSystem.Common;
    using LearnLanguageSystem.Data.Common.Models;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Contests = new HashSet<Contest>();
            this.AvatarUrl = GlobalConstants.DefaultUserAvatarUrl;
        }

        public string AvatarUrl { get; set; }

        public string RoomId { get; set; }

        public virtual Room Room { get; set; }

        public int PointsFromContests { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<Contest> Contests { get; set; }
    }
}
