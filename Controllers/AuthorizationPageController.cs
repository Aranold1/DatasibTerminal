using DataSibTerminal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace DataSibTerminal.Controllers
{
    [Route("/")]
    public class AuthorizationPageController : Controller
    {
        private readonly ILogger<AuthorizationPageController> _logger;
        //we gonna encapsulate it latter 
        private readonly postgresContext _postgresContext;

        public AuthorizationPageController(ILogger<AuthorizationPageController> logger, postgresContext postgresContext)
        {
            _logger = logger;
            _postgresContext = postgresContext;
           
        }
        public IActionResult LogIn(Users user)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("CreateTicket", "TicketCreationPage");
            }
            return View();
        }
    }
}