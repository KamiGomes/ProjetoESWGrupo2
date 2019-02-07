using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESW_Shelter.Models;
using Microsoft.AspNetCore.Mvc;

namespace ESW_Shelter.Controllers
{
    public class BackendController : SharedController
    {
        private readonly ShelterContext _context;

        public BackendController(ShelterContext context) : base(context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            if (!getPermissions().Any())
            {
                return NotFound();
            }
            ViewBag.Permission = getPermissions();
            return View();
        }
    }
}