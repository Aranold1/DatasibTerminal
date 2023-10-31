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
            string? Email = HttpContext.Response.Headers.Cookie.FirstOrDefault(x=>x.StartsWith("email"));
            string? Password = HttpContext.Response.Headers.Cookie.FirstOrDefault(x=>x.StartsWith("psswd"));
            System.Console.WriteLine(Email);
            System.Console.WriteLine(Password);
            if (ModelState.IsValid)
            {
                
                HttpContext.Response.Headers.Add("set-cookie", $"email=email:{user.Email}, password=pswwd:{user.Password}");
              

                return RedirectToAction("CreateTicket", "TicketCreationPage");
            }
            return View();
        }
        bool IsLoginAndPasswordValid()
        {
            return true;
        }
      
    }
}