using Microsoft.AspNetCore.Mvc;

namespace Swiggy.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
