using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESW_Shelter.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ESW_Shelter.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly ShelterContext _context;
        public AdministrationController(ShelterContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if(HttpContext.Session.GetString("UserID") == null)
            {
                return NotFound();
            }
            int getID = Int32.Parse(HttpContext.Session.GetString("UserID"));
            int role = _context.Users.Find(getID).RoleID;
            RoleAuthorization existAcess = _context.RoleAuthorization.Where(e => e.RoleFK == role).FirstOrDefault();
            if (existAcess != null)
            {
                return View();
            }
            return NotFound();
        }
    }
}