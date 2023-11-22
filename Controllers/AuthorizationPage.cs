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
        private readonly IDataProtectionProvider _dataProtectionProvider;
        public AuthorizationPage(ILogger<AuthorizationPage> logger, postgresContext postgresContext, IDataProtectionProvider idp)
        {
            _dataProtectionProvider = idp;
            _logger = logger;
            _postgresContext = postgresContext;
        }
        public async Task<IActionResult> LogIn(Users userForm)
        {
            var protector = _dataProtectionProvider.CreateProtector("auth-cookie");
            if (ModelState.IsValid)
            {

                var claims = new List<Claim>();
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
                claims.Add(new Claim(userForm.Email, usersDb.Name));
                var identity = new ClaimsIdentity(claims, "cookie");
                var user = new ClaimsPrincipal(identity);
                try
                {
                    bool res = await IsLoginAndPasswordValid(userForm.Email, userForm.Password);
                    if (res)
                    {
                        await HttpContext.SignInAsync("cookie", user);
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
            string apiUrl = "http://158.101.194.79:5003/api/authorization   "; //url 

            Users dataToSend = new Users
            {
                Name = "Class",
                Id = 777,
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