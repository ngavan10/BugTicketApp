using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
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

    [Route("api/ticket/{ticketId}/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
       
        
        private readonly IMapper _mapper;
        private readonly ITicketRepository _repo;

        public CommentController(DataContext context, ITicketRepository repo, IMapper mapper)
        {
            
            _mapper = mapper;
            _repo = repo;
        }

          [HttpGet("{id}", Name = "GetComment")]
        public async Task<IActionResult> GetComment(int ticketNumber, int id)
        {
            // if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            //     return Unauthorized();

          var photoFromRepo = await _repo.GetComment(id);

            var photo = _mapper.Map<CommentForReturnDto>(photoFromRepo);

            return Ok(photo);

            
            
        }

         
        [HttpPost]
        public async Task<IActionResult> CreateComment(CommentForCreationDto commentForCreationDto)
        {
            

            // if (creator.Id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            //     return Unauthorized();

         

            var comment = _mapper.Map<Comment>(commentForCreationDto);
            comment.UserId =  Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

              var regexItem = new Regex("^[a-zA-Z0-9 ]*$");

            if(!regexItem.IsMatch(comment.Description))
            {
                throw new Exception("Description invalid");
            }

            _repo.Add(comment);

           
            if (await _repo.SaveAll())
            {

                var commentToReturn = _mapper.Map<CommentForReturnDto>(comment);
                //commentToReturn.UserName = comment.User.Username;
                return CreatedAtRoute("GetComment",
                    new {comment.TicketId ,id = comment.Id}, commentToReturn);
            }
                 

                throw new Exception("Creating the comment failed on save");
            
        }

        // // GET api/values/5
        // [AllowAnonymous]
        // [HttpGet("{id}")]
        // public async Task<IActionResult> GetTicket(int id)
        // {
        //     //var value = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == id);
        //     //return Ok(value);

        // }

        

          [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTicket(int id, TicketForUpdateDto ticketForUpdateDto)
        {
         
            var ticketFromRepo = await _repo.GetTicket(id);

            _mapper.Map(ticketForUpdateDto, ticketFromRepo);    

            if (await _repo.SaveAll())
            return NoContent();

            throw new System.Exception($"Updating user {id} failed on save");
        }

       [HttpPost("{id}")]
        public async Task<IActionResult> DeleteComment(int id, int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var commentFromRepo = await _repo.GetComment(id);

            // if (commentFromRepo.UserId == userId)
            //     _repo.Delete(commentFromRepo);

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception("Error deleting message");
        }
    }
}
