using System;
using Microsoft.AspNetCore.Mvc;
using ESW_Shelter.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ESW_Shelter.Controllers
{
    public class SharedController : Controller
    {

        private readonly ShelterContext _context;

        public SharedController(ShelterContext context)
        {

            _context = context;
        }

        public bool GetAutorization(int roleid)
        {
            var user = _context.Users.Find(Int32.Parse(HttpContext.Session.GetString("UserID")));
            if (user == null)
            {
                return false;
            }
            if (user.RoleID == roleid)
            {
                return true;
            }
            return false;
        }

        public ActionResult ErrorNotFoundOrSomeOtherError()
        {
            TempData["Message"] = "Access Denied";
            return RedirectToAction("Index", "Home");
        }
    }
}