using System;
using System.Collections.Generic;

namespace BugTicketApp.API.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Description {get; set;}

        public int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
   
     

    }
}