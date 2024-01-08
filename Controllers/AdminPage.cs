using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
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
                Tickets = ticketList,
                Messages = messageList 
            };
            
            return View("Main", viewModel);
        }
        public async Task<IActionResult> ChangeChat(int TicketId)
        {
            
            var _messages = postgresContext.Messages.Where(x=>x.FkTicketId==TicketId).ToList();
            var _tickets = await postgresContext.Tickets.ToListAsync();
            var model = new MainViewModel
            {
              Messages = _messages,
              Tickets  = _tickets
                
            };
            return RedirectToAction("Main",model);
        }




        public async Task<IActionResult> SendMessage(Message message)
        {
            // бля егор это твое sasdada или я хуйнул 
            Console.WriteLine("sasdada");

            if (!string.IsNullOrEmpty(message.Body))
            {
                try
                {
                    var mes = await postgresContext.Messages.ToListAsync();
                    message.MessageId = mes.Count() + 1;
                    message.SendTime = DateTime.UtcNow;
                    message.UserRole = Convert.ToString(User.Claims.FirstOrDefault(x => x.Type == "User").Value);
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
        [Route("adduser")]
        public async Task<IActionResult> adduser(User usr)
        {
            if (ModelState.IsValid)
            {
                var name = usr.Name;
                name = name.Trim();
                if (!name.Contains(" "))
                {
                    ViewData["DoUserNameHaveSurname"] = false;
                    System.Console.WriteLine("user dont have a surname");
                    return View("adduser");
                }
                System.Console.WriteLine();
                System.Console.WriteLine("user is valid at adduser");
                usr.Id = postgresContext.Users.Count()+1;
                usr.Role="SimpleUser";
                if (!postgresContext.Users.Contains(usr))
                {
                    await postgresContext.AddAsync(usr);
                    await postgresContext.SaveChangesAsync();
                    return View("main");
                }
                else
                {
                    return View("adduser");
                }
                
            }
            return View("adduser");
        }

        

    }
}