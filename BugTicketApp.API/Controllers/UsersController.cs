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
    
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
       
        
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly ITicketRepository _repo;

        public UsersController(DataContext context, ITicketRepository repo, IMapper mapper)
        {
            
            _mapper = mapper;
            _repo = repo;
             _context = context;
        }


         [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var isCurrentUser = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value) == id;
            var user = await _repo.GetUser(id);
            var userToReturn = _mapper.Map<UserForReturnDto>(user);
            return Ok(userToReturn);
        }

    
        }

    
}
