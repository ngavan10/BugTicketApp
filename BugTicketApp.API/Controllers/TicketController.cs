using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BugTicketApp.API.Data;
using BugTicketApp.API.Dtos;
using BugTicketApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BugTicketApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
       
        
        private readonly IMapper _mapper;
        private readonly ITicketRepository _repo;

        public TicketController(DataContext context, ITicketRepository repo, IMapper mapper)
        {
            
            _mapper = mapper;
            _repo = repo;
        }

        // GET api/values
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetTickets()
        {
            var tickets = await _repo.GetTickets();
            var ticketsToReturn = _mapper.Map<IEnumerable<TicketForListDto>>(tickets);
            return Ok(ticketsToReturn);
        }



        

        [HttpGet("{id}", Name = "GetTicket")]
        public async Task<IActionResult> GetTicket(int id)
        {
        
          var ticketFromRepo = await _repo.GetTicket(id);
            var ticket = _mapper.Map<TicketForListDto>(ticketFromRepo);
            

            return Ok(ticket);

            
            
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket(TicketForCreationDto ticketForCreationDto)
        {

            var ticket = _mapper.Map<Ticket>(ticketForCreationDto);
            ticket.UserId =  Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
           
            _repo.Add(ticket);

           
            if (await _repo.SaveAll())
            {
                var ticketToReturn = _mapper.Map<TicketForListDto>(ticket);
                ticketToReturn.UserName = User.Identity.Name;
                ticketToReturn.TicketNumber = ticketToReturn.Id;
                return CreatedAtRoute("GetTicket",
                    new {id = ticket.Id}, ticketToReturn);
            }
                 

                throw new Exception("Creating the message failed on save");
            
        }

          [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTicket(int id, TicketForUpdateDto ticketForUpdateDto)
        {
         
            var ticketFromRepo = await _repo.GetTicket(id);

            _mapper.Map(ticketForUpdateDto, ticketFromRepo);    

            if (await _repo.SaveAll())
            return NoContent();

            throw new System.Exception($"Updating user {id} failed on save");
        }

        
    }
}
