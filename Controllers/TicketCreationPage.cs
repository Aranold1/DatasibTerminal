﻿using DataSibTerminal.Models;
using Microsoft.AspNetCore.Mvc;

namespace DataSibTerminal.Controllers
{
    public class TicketCreationPage : Controller
    {

        postgresContext postgresContext;

        public TicketCreationPage(postgresContext postgresContext)
        {
            this.postgresContext = postgresContext;
        }
        public async Task<IActionResult> CreateTicket(Ticket ticket)
        {
            Console.WriteLine(DateTime.Now);
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