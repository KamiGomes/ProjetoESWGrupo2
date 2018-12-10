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
            GetLogin();
            return View();
        }

        public IActionResult FrontEndLayout()
        {
            GetLogin();
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            GetLogin();
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            GetLogin();
            return View();
        }

        public IActionResult Privacy()
        {
            GetLogin();
            return View();
        }

        public IActionResult Register()
        {
            GetLogin();
            return View();
        }

        public IActionResult Account()
        {
            GetLogin();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            GetLogin();
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// <para>Método que redefine os session variables, para que não haja perda de dados. Verifica ainda se é admin ou não e devolve conforme o resultado.</para>
        /// </summary>
        /// <returns>
        /// <para> Caso a variável de sessão "Ad" não exista - false</para>
        /// <para> Caso a variável de sessão "Ad" exista - true</para>
        /// </returns>
        private bool GetLogin()
        {
            if (HttpContext.Session.GetString("User_Name") != null && HttpContext.Session.GetString("UserID") != null)
            {
                HttpContext.Session.SetString("User_Name", HttpContext.Session.GetString("User_Name"));
                HttpContext.Session.SetString("UserID", HttpContext.Session.GetString("UserID"));
            }
            if (HttpContext.Session.GetString("Ad") != null)
            {
                HttpContext.Session.SetString("Ad", HttpContext.Session.GetString("Ad"));
                return true;
            }
            return false;
        }
    }
}
