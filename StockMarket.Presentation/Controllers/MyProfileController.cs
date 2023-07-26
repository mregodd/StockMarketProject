using Microsoft.AspNetCore.Mvc;

namespace StockMarket.Presentation.Controllers
{
    public class MyProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
