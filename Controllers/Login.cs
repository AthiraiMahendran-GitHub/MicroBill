using Microsoft.AspNetCore.Mvc;

namespace MicroBill.Controllers
{
    public class Login : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
