using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESW_Shelter.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ESW_Shelter.Controllers
{
    public class AdministrationController : SharedController
    {
        private readonly ShelterContext _context;
        public AdministrationController(ShelterContext context) : base(context)
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