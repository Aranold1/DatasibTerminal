using DataSibTerminal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.AspNetCore.DataProtection;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;


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
                            new Claim(ClaimTypes.NameIdentifier, usersDb.Email),
                            new Claim("User", "SimpleUser")
                        });
                        
                        var identity = new ClaimsIdentity(claims,"cookie",nameType:null,roleType:"User");
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
            string apiUrl = "http://158.101.194.79:5003/api/authorization  "; //url 

            Users_DTO dataToSend = new Users_DTO
            {
                Email = email,
                Password = password,
            };

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(JsonConvert.SerializeObject(dataToSend), System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<bool>(responseBody);

                }
                else
                {
                    Console.WriteLine("Error sending data to the web API. Status code: " + response.StatusCode);
                }
            }
            return false;
        }

    }
}