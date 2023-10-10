using Microsoft.AspNetCore.Mvc;

namespace DataSibTerminal.Controllers
{
    public class TicketCreationController : Controller
    {
        public IActionResult TicketCreationPage()
        {
            return View();
        }
    }
}
