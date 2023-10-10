using Microsoft.AspNetCore.Mvc;

namespace DataSibTerminal.Controllers
{

   
    public class TicketCreationPage : Controller
    {
        public IActionResult TicketPage()
        {
            return View();
        }
    }
}
