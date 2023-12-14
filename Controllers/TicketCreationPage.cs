using System.ComponentModel.DataAnnotations;
using DataSibTerminal.Models;
using DataSibTerminal.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace DataSibTerminal.Controllers
{
    [Route("yourtickets")]
    
    [Authorize(Roles = "SimpleUser")]
    public class TicketCreationPage : Controller
    {

        postgresContext postgresContext;

        public TicketCreationPage(postgresContext postgresContext)
        {
            this.postgresContext = postgresContext;
        }
        public async Task<IActionResult> CreateTicket(Ticket ticket)
        {
            string name = "";
            foreach (var item in User.Claims.ToList())
            {
                await Console.Out.WriteLineAsync(item.Value);
                if (item.Value.Contains("User:"))
                {

                    await Console.Out.WriteLineAsync(item.Value);
                    name = item.Value.Split("User:").Last();
                }
            }
            await Console.Out.WriteLineAsync("output");
            await Console.Out.WriteLineAsync(name);
            ticket.username = name;
            long time = long.Parse(new string(DateTime.Now.ToString().Where(x => char.IsDigit(x)).ToArray()));
            ticket.CreationTime = time;
            
            if (ModelState.IsValid)
            {
                postgresContext.Add(ticket);
                await postgresContext.SaveChangesAsync();
            }
            return View();
        }
    }
}
