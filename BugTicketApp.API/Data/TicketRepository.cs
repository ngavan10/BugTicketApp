using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTicketApp.API.Data;
using BugTicketApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BugTicketApp.API.Data
{
    public class TicketRepository : ITicketRepository
    {
        private readonly DataContext _context;
        public TicketRepository(DataContext context)
        {
            _context = context;

        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Ticket> GetTicket(int id)
        {
            return await _context.Tickets.Include(c => c.Comments).Include(u => u.User).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Ticket>> GetTickets()
        {
              var tickets = await _context.Tickets.Include(c => c.Comments).Include(u => u.User).ToListAsync();
              return tickets;     
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<Comment> GetComment(int id)
        {
            return await _context.Comments.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }


        public async Task<Comment> GetTicketComment(int id)
        {
             return await _context.Comments.FirstOrDefaultAsync();
        }
    }

}