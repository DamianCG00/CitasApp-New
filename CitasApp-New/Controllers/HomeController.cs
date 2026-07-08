using Microsoft.AspNetCore.Mvc;

namespace CitasApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult Privacy() => View();
    }
}
