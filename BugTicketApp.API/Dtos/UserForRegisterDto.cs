using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BugTicketApp.API.Models;

namespace BugTicketApp.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You must specify password between 4 and 8 characters.")]
        public string Password { get; set; }


       
     
    }
}
