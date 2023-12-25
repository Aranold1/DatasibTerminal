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

        [Route("main")]
        public async Task<IActionResult> Main()
        {
            var ticketList = await postgresDb.Ticket.ToListAsync();
            ticketList.Reverse();
            return View("Main",ticketList);
        }
    }
}
