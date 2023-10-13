using DataSibTerminal.Models;
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
        public IActionResult CreateTicket(Ticket ticket)
        {
            long time = long.Parse(new string(DateTime.Now.ToString().Where(x => char.IsDigit(x)).ToArray()));
            Console.WriteLine(time);
            Console.WriteLine(ticket.Description);
            Console.WriteLine(ticket.AnydeskId);
            ticket.CreationTime = time;
            postgresContext.Add(ticket);
            postgresContext.SaveChanges();

            return View();
        }
    }
}
