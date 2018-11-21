using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ESW_Shelter.Models;
using ESW_Shelter.Controllers;
using Microsoft.AspNetCore.Http;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;
//hotfix -> Install-Package Microsoft.AspNet.Mvc -Version 5.2.3.0 | Install-Package httpsecurecookie -Version 0.1.1 | Install-Package Microsoft.AspNetCore.Session -Version 2.1.1 
namespace ESW_Shelter.Controllers
{
    public class UsersController : Controller
    {
        private readonly ShelterContext _context;
        private readonly IConfiguration _configuration;

        public UsersController(ShelterContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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
        public async Task<IActionResult> Create([Bind("UserID,Email,Name,Password,RoleID,ConfirmedEmail")] Users users)
        {
            if (ModelState.IsValid)
            {

                if (!_context.Users.Any(x => x.Email == users.Email))
                {
                    UsersInfo newUserInfo = new UsersInfo();
                    /** Password encrypting ***/
                    /*var data = Encoding.ASCII.GetBytes(users.Password);
                    var sha256 = new SHA256CryptoServiceProvider();
                    var sha256data = sha256.ComputeHash(data);
                    users.Password = sha256data;*/
                    /** End of Password encrypting **/
                    _context.Add(users);
                    _context.SaveChanges();
                    newUserInfo.UserID = _context.Users.Max(user => users.UserID);
                    _context.Add(newUserInfo);
                    await _context.SaveChangesAsync();
                    /** Send Confirmation Email **/
                    NotificationSender sender = new NotificationSender(_configuration);
                    int user_id = (from user in _context.Users select user.UserID).Max();
                    string link = String.Format("<h3><a href=\"https://localhost:44359/Users/ConfirmEmail/{0}\">Click here to confirm your account so you can login with it!</a></h3>", user_id);
                    string subj = "Welcome to our Shelter " + users.Name + "!";
                    string content = "<h1>We, ESW Group 2 Welcome you to our project!</h1>" +
                        "<p><h2>Please, to continue with your registration, we ask that you verify your account in the following link:</h2></p>"+
                        link +
                        "<p><h2>Any questions can be sent to this same email. I hope you enjoy the experience</h2></p>";
                    await sender.PostMessage(subj, content, users.Email, users.Name);
                    /** End of Confirmation Email **/
                    TempData["Message"] = "Success creating User!";
                    return View("~/Views/Home/Index.cshtml");
                }
                else
                {

                    ModelState.AddModelError("Email", "Email already exists!");
                    return View("~/Views/Home/Account.cshtml");
                }
            }
            return View("~/Views/Home/Index.cshtml");
        }

        //Post Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,Password")] Users users)
        {
            try
            {
                var userRetrieved = (from user in _context.Users where user.Email == users.Email && user.Password == users.Password select user).First();

                if (userRetrieved != null)
                {
                    if (userRetrieved.ConfirmedEmail == false)
                    {
                        TempData["Message"] = "Id: "+userRetrieved.UserID.ToString()+"| Confirmed: "+userRetrieved.ConfirmedEmail.ToString() ;
                        return View("~/Views/Home/Index.cshtml");
                    }
                    HttpContext.Session.SetString("User_Name", userRetrieved.Name);
                    HttpContext.Session.SetString("UserID", userRetrieved.UserID.ToString());
                    TempData["Message"] = "Success Logging In User!";
                    return View("~/Views/Home/Index.cshtml");
                }
                else
                {
                    ModelState.AddModelError("Email", "Email or Password incorrect!");
                    return View(("~/Views/Homes/Account.cshtml"));
                }
            }
            catch (InvalidOperationException e)
            {
                ModelState.AddModelError("Email", "Email or Password incorrect!");
                return View("~/Views/Homes/Account.cshtml");
            }

        }
        //[Route("")]
        // GET: Users/Edit/5
        public async Task<IActionResult> ConfirmEmail(int? id)
        {
            if (id == null)
            {
                TempData["Message"] = "Page not allowed!";
                return View("~/Views/Home/Index.cshtml");
            }

            Users users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                TempData["Message"] = "User not Found!";
                return View("~/Views/Home/Index.cshtml");
            }
            users.ConfirmedEmail = true;
            _context.Update(users);
            await _context.SaveChangesAsync();
            /*var usersInfo = _context.UsersInfo.Where(ui => ui.UserID == id);
            ViewModel mymodel = new ViewModel();
            mymodel.User = users;
            mymodel.UserInfo = (UsersInfo) usersInfo;*/
            TempData["Message"] = "Account activated! Proceed to login in!";
            return View("~/Views/Home/Index.cshtml");
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
