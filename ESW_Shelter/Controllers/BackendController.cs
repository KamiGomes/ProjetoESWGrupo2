using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ESW_Shelter.Controllers
{
    public class BackendController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}