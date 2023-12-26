using System.ComponentModel.DataAnnotations;
using DataSibTerminal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using System.Linq;


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

        public async Task<IActionResult> SendMassage(Massages massages)
        {
            if (ModelState.IsValid)
            {
                var claims = User.Claims.ToList();
                try
                {
                   
                    massages.send_time = DateTime.UtcNow;
                    massages.massage
                    ticket.Username = Name;
                }
                catch
                {

                    System.Console.WriteLine("cant parse ");
                }
                try
                {

                    postgresDb.Add(ticket);
                    await postgresDb.SaveChangesAsync();
                }
                catch
                {
                    System.Console.WriteLine("some data are wrong");
                }
            }
            return View();
        }
    }
}
