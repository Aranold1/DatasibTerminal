using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataSibTerminal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DataSibTerminal.Controllers
{
    public class AdminPage : Controller
    {
        private readonly postgresContext tickets;
        private readonly Massages massage;

        public AdminPage(postgresContext pg, Massages m)
        {
            tickets = pg;
            massage = m;
        }

        [Route("main")]
        public async Task<IActionResult> Main()
        {
            
            var ticketList = await tickets.Ticket.ToListAsync();
            ticketList.Reverse();

            var viewModel = new TicketMassageViewModel
            {
                Tickets = ticketList,
                Massage = massage
            };

            return View("Main", viewModel);
        }

        [HttpPost] 
        public async Task<IActionResult> SendMassage(Massages massages)
        {
            if (ModelState.IsValid)
            {
               
                try
                {
                    massages.send_time = DateTime.UtcNow;
                }
                catch
                {
                    System.Console.WriteLine("cant parse ");
                }
                try
                {
                    tickets.Massages.Add(massages);
                    await tickets.SaveChangesAsync();
                }
                catch
                {
                    System.Console.WriteLine("some data are wrong");
                }
            }
            return RedirectToAction("Main"); 
        }
    }
}
