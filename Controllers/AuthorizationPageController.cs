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
        private readonly HttpContext _httpContext; 

        public AuthorizationPageController(ILogger<AuthorizationPageController> logger, postgresContext postgresContext,HttpContext httpContext)
        {
            _logger = logger;
            _postgresContext = postgresContext;
            _httpContext = httpContext;
        }
        public IActionResult LogIn(Users user)
        {
            if(_httpContext.Request.Headers.Cookie.FirstOrDefault(x=>x.StartsWith("psswd="))!=null&&
            _httpContext.Request.Headers.Cookie.FirstOrDefault(x=>x.StartsWith("login="))!=null)
            {


            }
            if (ModelState.IsValid)
            {
                return RedirectToAction("CreateTicket", "TicketCreationPage");
            }
            return View();
        }
        bool IsPasswordValid(string Password)
        {
                
        }
    }
}