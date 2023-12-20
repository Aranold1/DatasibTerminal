using System.ComponentModel.DataAnnotations;
using DataSibTerminal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;


namespace DataSibTerminal.Controllers
{
    public class AdminPage :Controller
    {
        postgresContext postgresDb;


        public AdminPage(postgresContext pg)
        {
            postgresDb = pg;

        }

        [HttpGet("main")]
        public async Task<IActionResult> Main()
        {

            Task<List<Ticket>> ticketListTask = postgresDb.Ticket.ToListAsync();

            
            List<Ticket> ticketList = await ticketListTask;
            ticketList.Reverse();
            ViewData["TicketList"] = ticketList;



            return View("Main");
        }
    }
}
