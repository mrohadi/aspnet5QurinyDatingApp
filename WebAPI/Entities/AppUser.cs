using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace WebAPI.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public DateTime DateOfBirth { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime LastActive { get; set; } = DateTime.Now;
        public string Gender { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<Photo> Photos { get; set; }

        // Many to many relationship for like feature
        public ICollection<UserLike> LikedByUsers { get; set; }
        public ICollection<UserLike> LikedUsers { get; set; }

        // Many to many relationship for message feature
        public ICollection<Message> MessageSent { get; set; }
        public ICollection<Message> MessageRecived { get; set; }

        // Many to many relationship for Identity Role 
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}