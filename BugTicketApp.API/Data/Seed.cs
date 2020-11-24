using System.Collections.Generic;
using System.Linq;
using BugTicketApp.API.Models;
using Newtonsoft.Json;

namespace BugTicketApp.API.Data
{
    public class Seed
    {
        public static void SeedUsers(DataContext context)
        {
            if(!context.Users.Any())
            {
                var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);
                foreach(var user in users)
                {
                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHash("password", out passwordHash, out passwordSalt);

                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                    user.Username = user.Username.ToLower();
                    context.Users.Add(user);
                }
                context.SaveChanges();
            }
        }

         public static void SeedTickets(DataContext context)
        {
            if(!context.Tickets.Any())
            {
                var ticketData = System.IO.File.ReadAllText("Data/TicketSeedData.json");
                var tickets = JsonConvert.DeserializeObject<List<Ticket>>(ticketData);
                foreach(var ticket in tickets)
                {
                    ticket.TicketNumber = ticket.TicketNumber;
                    ticket.Description = ticket.Description;
                    ticket.Status = ticket.Status;
                    ticket.Priority = ticket.Priority;
                    ticket.UserId = ticket.UserId;
                    context.Tickets.Add(ticket);
                }
                context.SaveChanges();
            }
        }

          private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512()){
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            
        }
    }
}