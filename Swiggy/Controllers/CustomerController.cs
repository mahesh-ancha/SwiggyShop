using Microsoft.AspNetCore.Mvc;

namespace Swiggy.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
