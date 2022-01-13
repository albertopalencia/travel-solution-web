using Microsoft.AspNetCore.Mvc;

namespace TravelWeb.Client.Controllers
{
    public class HomeController : Controller
    { 
        public IActionResult Index()
        {
            return View();
        }
         
    }
}
