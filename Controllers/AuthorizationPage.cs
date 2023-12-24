using DataSibTerminal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.AspNetCore.DataProtection;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace DataSibTerminal.Controllers
{
    [Route("/")]
    public class AuthorizationPage : Controller
    {
        
        
        private readonly ILogger<AuthorizationPage> _logger;
        //we gonna encapsulate it latter 
        private readonly postgresContext _postgresContext;
        public AuthorizationPage(ILogger<AuthorizationPage> logger, postgresContext postgresContext, IDataProtectionProvider idp)
        {
            _logger = logger;
            _postgresContext = postgresContext;
        }
        public async Task<IActionResult> LogIn(Users userForm)
        {
            var UserClaims = User.Claims;
            if (User.IsInRole("SimpleUser"))
            {
                return RedirectToAction("CreateTicket", "TicketCreationPage");
            }
            
            if (ModelState.IsValid)
            {

               
                //some really bad code
                var usersDb = new Users();
                try
                {
                    usersDb = _postgresContext.Users.FirstOrDefault(x => x.Email == userForm.Email);
                }
                catch
                {
                    await Console.Out.WriteLineAsync("db is dead");
                    return View("Login");
                }
                if (usersDb is null)
                {
                    await Console.Out.WriteLineAsync("user is null");
                    return View("Login");
                }
                
                try
                {
                    bool res = await IsLoginAndPasswordValid(userForm.Email, userForm.Password);
                    if (res)
                    {
                        var claims = new List<Claim>(new Claim[]
                        {
                            new Claim("Id",usersDb.Id.ToString()),
                            new Claim("Name",usersDb.Name),
                            new Claim(ClaimTypes.NameIdentifier, usersDb.Email),
                            new Claim("User", "SimpleUser"),

                        });

                        var identity = new ClaimsIdentity(claims, "cookie", nameType: null, roleType: "User");
                        var user = new ClaimsPrincipal(identity);
                        await HttpContext.SignInAsync(user);
                        return RedirectToAction("CreateTicket", "TicketCreationPage");
                    }
                }
                catch
                {
                    await Console.Out.WriteLineAsync("auth microservies is dead");
                    return View("Login");
                }
            }
            return View();
        }

        async Task<bool> IsLoginAndPasswordValid(string email, string password)
        {
            
            await foreach (var u in _postgresContext.Users)
            {

                if (u.Password == password && u.Email == email)
                {
                   
                    return true;
                }
            }
            return false;
        }

    }
}