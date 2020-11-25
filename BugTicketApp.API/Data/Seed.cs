using System.Collections.Generic;
using System.Linq;
using BugTicketApp.API.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace BugTicketApp.API.Data
{
    public class Seed
    {
        public static void SeedUsers(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            if(!userManager.Users.Any())
            {
                var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);

                var roles = new List<Role>
                {
                    new Role{Name = "Member"},
                    new Role{Name = "Admin"},
                    new Role{Name = "Moderator"},
                    new Role{Name = "VIP"}
                };

                foreach (var role in roles)
                {
                    roleManager.CreateAsync(role).Wait();
                }

                foreach(var user in users)
                {
                    userManager.CreateAsync(user, "password").Wait();
                    userManager.AddToRoleAsync(user, "Member");
                }

                var adminUser = new User
                {
                    UserName = "Admin"
                };

                var result = userManager.CreateAsync(adminUser, "password").Result;

                if (result.Succeeded)
                {
                    var admin = userManager.FindByNameAsync("Admin").Result;
                    userManager.AddToRolesAsync(admin, new[] {"Admin", "Moderator"});
                }
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