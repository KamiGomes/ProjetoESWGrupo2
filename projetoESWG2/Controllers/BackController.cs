using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace projetoESWG2.Controllers
{
    public class BackController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}