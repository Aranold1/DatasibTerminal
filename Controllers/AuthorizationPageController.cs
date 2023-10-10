using DataSibTerminal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
        public IActionResult LogIn(User user)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("TicketCreation", "TicketCreationPage");
            }
            return View();
        }
    }
}