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
        private readonly PostgresContext postgresContext;

        public AdminPage(PostgresContext pg)
        {
            postgresContext = pg;
        }

        [Route("main")]
        public async Task<IActionResult> Main()
        {
            
            var ticketList = await postgresContext.Tickets.ToListAsync();
            var massage = await postgresContext.Messages.ToListAsync();
            ticketList.Reverse();
            var viewModel = new TicketMassageViewModel{
                Massage = massage,
                Tickets = ticketList
            };
            return View("Main", viewModel);
        }

        [HttpPost] 
        public async Task<IActionResult> SendMassage(Message messages)
        {
            if (ModelState.IsValid)
            {
               
                try
                {
                    messages.SendTime = DateTime.UtcNow;
                }
                catch
                {
                    System.Console.WriteLine("cant parse ");
                }
                try
                {
                    
                    postgresContext.Messages.Add(messages);
                    await postgresContext.SaveChangesAsync();
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