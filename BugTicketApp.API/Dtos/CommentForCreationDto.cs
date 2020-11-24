using System;

namespace BugTicketApp.API.Dtos
{
    public class CommentForCreationDto
    {
       //public int UserId { get; set; }
       public DateTime CommentCreated { get; set; }
        public string Description { get; set; }
        public int TicketId {get; set;}
    
        public CommentForCreationDto()
        {
            CommentCreated = DateTime.Now;
        }
    }
}