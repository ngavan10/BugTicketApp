using System;
using System.Collections.Generic;

namespace BugTicketApp.API.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        public int TicketNumber {get; set;}
        public string Description {get; set;}
        public string Status {get;set;}
        public string Priority {get;set;}
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
   
     

    }
}