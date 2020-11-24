using System;

namespace BugTicketApp.API.Dtos
{
    public class CommentForReturnDto
    {
       public int Id { get; set; }
       public DateTime CommentCreated { get; set; }
        public string Description { get; set; }
        public string Username {get; set;}
        public int TicketId {get; set;}
       
    }
}