using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ESW_Shelter.Models;
using Microsoft.AspNetCore.Http;

//hotfix -> Install-Package Microsoft.AspNet.Mvc -Version 5.2.3.0 | Install-Package httpsecurecookie -Version 0.1.1 | Install-Package Microsoft.AspNetCore.Session -Version 2.1.1 
namespace ESW_Shelter.Controllers
{
    public class UsersController : Controller
    {
        private readonly ShelterContext _context;

        public UsersController(ShelterContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }
  
        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,Email,Name,Password")] Users users)
        {
            if (ModelState.IsValid)
            {

                if (!_context.Users.Any(x => x.Email == users.Email))
                {
                    UsersInfo newUserInfo = new UsersInfo();
                    _context.Add(users);
                    _context.SaveChanges();
                    newUserInfo.UserID = _context.Users.Max(user => users.UserID);
                    _context.Add(newUserInfo);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Success creating User!";
                    return View("~/Views/Home/Index.cshtml");
                }
                else
                {
                    
                    ModelState.AddModelError("Email", "Email already exists!");
                    return View(users);
                }
            }
            return View("~/Views/Home/Index.cshtml");
        }

        //Post Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,Password")] Users users)
        {
            String user_name = (from user in _context.Users where user.Email == users.Email && user.Password == users.Password select user.Name).First();
            int user_id = (from user in _context.Users where user.Email == users.Email && user.Password == users.Password select user.UserID).First();

            if (user_name != null)
                {
                    HttpContext.Session.SetString("User_Name", user_name);
                    HttpContext.Session.SetString("UserID", user_id.ToString());
                    TempData["Message"] = "Success Logging In User!";
                    return View("~/Views/Home/Index.cshtml");
                }
                else
                {
                    ModelState.AddModelError("Email", "Email or Password incorrect!");
                    return View(users);
                }
            
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            /*var usersInfo = _context.UsersInfo.Where(ui => ui.UserID == id);
            ViewModel mymodel = new ViewModel();
            mymodel.User = users;
            mymodel.UserInfo = (UsersInfo) usersInfo;*/

            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserID,Email,Name,Password")] Users users)
        {
            if (id != users.UserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.UserID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var users = await _context.Users.FindAsync(id);
            _context.Users.Remove(users);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.UserID == id);
        }
    }
}
