using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ESW_Shelter.Models;
using Microsoft.AspNetCore.Http;

namespace ESW_Shelter.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Menu()
        {
            return PartialView("~/Views/Shared/_Menu.cshtml");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AdministrationAcess()
        {
            return View();
        }

        public IActionResult FrontEndLayout()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Account()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
