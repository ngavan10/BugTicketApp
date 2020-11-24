using System.Collections.Generic;
using System.Threading.Tasks;
using BugTicketApp.API.Models;

namespace BugTicketApp.API.Data
{
    public interface ITicketRepository
    {
         void Add<T>(T entity) where  T: class;
         void Delete<T>(T entity) where  T: class;
         Task<bool> SaveAll();

         Task<IEnumerable<Ticket>> GetTickets();

         Task<Ticket> GetTicket(int id);

         Task<User> GetUser(int id);
        Task <Comment> GetComment(int id);

        Task<Comment> GetTicketComment(int id);
    }
}