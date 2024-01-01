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
            var messageList = await postgresContext.Messages.ToListAsync();
            ticketList.Reverse();

            var viewModel = new MainViewModel
            {
                ticket = new Ticket(),
                message = new Message()
            };

            ViewData["ticket"] = ticketList;  
            ViewData["message"] = messageList;

            return View("Main", viewModel);
        }




        public async Task<IActionResult> SendMessage(Message message)
        {
            Console.WriteLine("sasdada");

            if (!string.IsNullOrEmpty(message.Message1))
            {
                try
                {
                    var mes = await postgresContext.Messages.ToListAsync();
                    message.MessageId = mes.Count() + 1;
                    message.SendTime = DateTime.UtcNow;
                }
                catch
                {
                    System.Console.WriteLine("cant parse ");
                }

                try
                {
                    postgresContext.Messages.Add(message);
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