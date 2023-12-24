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
            if (ModelState.IsValid)
            {
                var claims = User.Claims.ToList();
                var Name = claims.Where(x => x.Type == "Id").Select(x => x.Value).First();
                var Id = int.Parse(claims.Where(x => x.Type == "Id").Select(x => x.Type).First());
                long time = long.Parse(new string(DateTime.Now.ToString().Where(x => char.IsDigit(x)).ToArray()));
                ticket.CreationTime = time;
                postgresContext.Add(ticket);
                await postgresContext.SaveChangesAsync();
            }
            return View();
        }
    }
}
