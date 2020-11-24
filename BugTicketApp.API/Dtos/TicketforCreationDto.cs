using System;

namespace BugTicketApp.API.Dtos
{
    public class TicketForCreationDto
    {
       public int TicketNumber { get; set; }
       public DateTime TicketCreated { get; set; }
        public string Description { get; set; }
        public string Priority { get; set;}
        public string Status { get; set; }
        public int UserId {get; set;}
    
        public TicketForCreationDto()
        {
            TicketCreated = DateTime.Now;
        }
    }
}