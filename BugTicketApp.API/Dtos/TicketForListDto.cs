using System;
using System.Collections.Generic;
using BugTicketApp.API.Models;

namespace BugTicketApp.API.Dtos
{
    public class TicketForListDto
    {
       public int Id { get; set; }

        public int TicketNumber {get; set;}
        public string Description {get; set;}

        public string AssignedTo { get; set; }
        public string Status {get;set;}
        public string Priority {get;set;}
        public ICollection<CommentForReturnDto> Comments { get; set; }
        public string UserName {get; set;}
    }
}