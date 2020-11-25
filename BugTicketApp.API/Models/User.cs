using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace BugTicketApp.API.Models
{
    public class User : IdentityUser<int>
    {
        // public int Id { get; set; }
        // public string Username { get; set; }
        // public byte[] PasswordHash { get; set; }
        // public byte[] PasswordSalt { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
        
     

    }
}