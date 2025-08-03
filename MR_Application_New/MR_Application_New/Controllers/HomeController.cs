using Microsoft.AspNetCore.Mvc;

namespace MR_Application_New.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult mohan()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Error(string message)
        {
            ViewData["ErrorMessage"] = message;
            return View(); // Return the Error view
        }

        [HttpGet]

        public IActionResult Denied(string message)
        {
            ViewBag.ErrorMessage = message;
            return View();
        }
    }
}
