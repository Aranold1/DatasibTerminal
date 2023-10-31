using DataSibTerminal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml.Linq;


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


       async Task<bool> IsLoginAndPasswordValid(string Email,string Password)
        {
            string apiUrl = "http://158.101.194.79:5003/api/authorization   "; //url 

            Users dataToSend = new Users
            {
                Id = 1,
                Name = "sanek",
                Email = "jmohn@exaple.com",
                Password = "johns_password",
            };

            using (var httpClient = new HttpClient())
           {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(JsonConvert.SerializeObject(dataToSend), System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Successfully sent data to the web API.");
                    Console.WriteLine("Server response: " + responseBody);
                }
                else
                {
                    Console.WriteLine("Error sending data to the web API. Status code: " + response.StatusCode);
                }
                return res;
            }
        }
      
    }
}