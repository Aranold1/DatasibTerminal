using System.ComponentModel.DataAnnotations;
using DataSibTerminal.Models;
using DataSibTerminal.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Security.Cryptography;

    namespace DataSibTerminal.Controllers
    {
        [Route("yourtickets")]
        
        [Authorize(Roles = "SimpleUser")]
        public class TicketCreationPage : Controller
        {

            readonly postgresContext postgresContext;

            public TicketCreationPage(postgresContext postgresContext)
            {
                this.postgresContext = postgresContext;
            }
            public async Task<IActionResult> CreateTicket(Ticket ticket)
            {
                if (ModelState.IsValid)
                {
                    var claims = User.Claims.ToList();
                    try
                    {
                        var Name = claims.Where(x => x.Type == "Name").Select(x => x.Value).First();
                        var Id = int.Parse(claims.Where(x => x.Type == "Id").Select(x => x.Value).First());
                        ticket.UserId = Id;
                        ticket.CreationTime = DateTime.UtcNow;
                        //to do: mark key as serial to avoid this request in future 
                        // ps: i dont know how to mark key as serial in entity orm
                        ticket.ticket_id = postgresContext.Ticket.Max(x=>x.ticket_id)+1;
                        ticket.Username = Name;
                    }
                    catch{
                       
                        System.Console.WriteLine("cant parse ");
                    }
                    try{

                        postgresContext.Add(ticket);
                        await postgresContext.SaveChangesAsync();
                    }
                    catch{
                        System.Console.WriteLine("some data are wrong");
                    }
                }
                return View();
            }
        }
    }
