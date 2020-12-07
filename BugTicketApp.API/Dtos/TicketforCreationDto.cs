using System;
using System.ComponentModel.DataAnnotations;

namespace BugTicketApp.API.Dtos
{
    public class TicketForCreationDto
    {
       public int TicketNumber { get; set; }
       public DateTime TicketCreated { get; set; }
       [Required]
        public string Description { get; set; }
        [Required]
        public string Priority { get; set;}
        [Required]
        public string Status { get; set; }
     
        public int UserId {get; set;}
    
        public TicketForCreationDto()
        {
            TicketCreated = DateTime.Now;
        }
    }
}