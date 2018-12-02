using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ESW_Shelter.Models;
using Microsoft.AspNetCore.Http;

namespace ESW_Shelter.Controllers
{
    public class HomeController : Controller
    {
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


        public IActionResult Account()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private void GetLogin()
        {
            if(HttpContext.Session.GetString("User_Name") != null && HttpContext.Session.GetString("UserID") != null)
            {
                HttpContext.Session.SetString("User_Name", HttpContext.Session.GetString("User_Name"));
                HttpContext.Session.SetString("UserID", HttpContext.Session.GetString("UserID"));
            }
        }
    }
}
