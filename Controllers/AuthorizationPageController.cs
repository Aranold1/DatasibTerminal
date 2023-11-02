using DataSibTerminal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.AspNetCore.DataProtection;


namespace DataSibTerminal.Controllers
{
    [Route("/")]
    public class AuthorizationPageController : Controller
    {
        private readonly ILogger<AuthorizationPageController> _logger;
        //we gonna encapsulate it latter 
        private readonly postgresContext _postgresContext;
        private readonly IDataProtectionProvider _dataProtectionProvider;
        public AuthorizationPageController(ILogger<AuthorizationPageController> logger, postgresContext postgresContext,IDataProtectionProvider idp)
        {
            _dataProtectionProvider = idp;
            _logger = logger;
            _postgresContext = postgresContext;
        }
        public async Task<IActionResult> LogIn(Users user)
        {
          

            
            if (ModelState.IsValid)
            {
                var protector = _dataProtectionProvider.CreateProtector("auth-cookie");
                Response.Cookies.Append("email",$"{protector.Protect(user.Email)}");
                Response.Cookies.Append("psswd",$"{protector.Protect(user.Password)}");
                return RedirectToAction("CreateTicket", "TicketCreationPage");
            }
            return View();
        }


        async Task<bool> IsLoginAndPasswordValid(string email,string password)
        {
            string apiUrl = "http://158.101.194.79:5003/api/authorization   "; //url 

            Users dataToSend = new Users
            {
                //сделай метод который получает айди и имя по почте
                Id = 1,
                Name = "sanek",
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
                    Console.WriteLine("Successfully sent data to the web API.");
                    Console.WriteLine("Server response: " + responseBody);
                }
                else
                {
                    Console.WriteLine("Error sending data to the web API. Status code: " + response.StatusCode);
                }
                //заглушка чтобы комплилось 
                return true;
                //return res;
            }
        }
      
    }
}