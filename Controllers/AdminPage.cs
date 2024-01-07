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
           
            ticketList.Reverse();

            var viewModel = new MainViewModel
            {
                ticket = new Ticket(),
                message = new Message()
            };

            ViewData["ticket"] = ticketList;  
           

            return View("Main", viewModel);
        }




        public async Task<IActionResult> SendMessage(Message message)
        {
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

        public async Task<IActionResult> ChangeChat(int id)
        {
            var messageList = await postgresContext.Messages.ToListAsync();
            Console.WriteLine("��� � ������");
            ViewData["message"] = messageList.Where(x => x.FkTicketId == id).ToList();


            ViewData["ChatId"] = id;
            

            return RedirectToAction("Main");
        }

    }
}