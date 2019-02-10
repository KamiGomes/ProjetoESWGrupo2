using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ESW_Shelter.Models;
using System;
using Microsoft.AspNetCore.Http;

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

        public bool GetAuthorization(int component, char type)
        {
            int id = Int32.Parse(HttpContext.Session.GetString("UserID"));
            int roleOfID = _context.Users.Find(id).RoleID;

            var authorization = _context.RoleAuthorization.Where(e => e.RoleFK == roleOfID && e.ComponentFK == component);

            if (authorization == null)
            {
                return false;
            }

            switch (type)
            {
                case 'c':
                    var result = authorization.Where(e => e.Create == true).FirstOrDefault();
                    if (result != null)
                    {
                        return true;
                    }
                    return false;

                case 'r':
                    var result2 = authorization.Where(e => e.Read == true).FirstOrDefault();
                    if(result2 != null)
                    {
                        return true;
                    }
                    return false;

                case 'u':
                    var result3 = authorization.Where(e => e.Update == true).FirstOrDefault();
                    if (result3 != null)
                    {
                        return true;
                    }
                    return false;

                case 'd':
                    var result4 = authorization.Where(e => e.Delete == true).FirstOrDefault();
                    if (result4 != null)
                    {
                        return true;
                    }
                    return false;

                default:
                    return false;
            }

        }

        public IQueryable<RoleAuthorization> getPermissions()
        {
            int id = Int32.Parse(HttpContext.Session.GetString("UserID"));
            return _context.RoleAuthorization.Where(e => e.RoleFK == _context.Users.Where(f => f.UserID == id).Select(f => f.RoleID).First());

        }

        public ActionResult ErrorNotFoundOrSomeOtherError()
        {
            TempData["Message"] = "Access Denied";
            return RedirectToAction("Index", "Home");
        }
    }
}