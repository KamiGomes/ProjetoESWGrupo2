using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ESW_Shelter.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using ESW_Shelter.Data;
using RestSharp;
using RestSharp.Authenticators;

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

        // Backoffice - GET: Users
        public async Task<IActionResult> Index()
        {
            var userProfile = (from user in _context.Users
                               join userInfo in _context.UsersInfo on user.UserID equals userInfo.UserID
                               select new
                               {
                                   user.UserID,
                                   user.Email,
                                   user.Password,
                                   user.Name,
                                   user.ConfirmedEmail,
                                   user.RoleID,
                                   userInfo.UserInfoID,
                                   userInfo.Street,
                                   userInfo.PostalCode,
                                   userInfo.City,
                                   userInfo.Phone,
                                   userInfo.AlternativePhone,
                                   userInfo.AlternativeEmail,
                                   userInfo.Facebook,
                                   userInfo.Twitter,
                                   userInfo.Instagram,
                                   userInfo.Tumblr,
                                   userInfo.Website
                               }).AsEnumerable().Select(x => new Profile
                               {
                                   UserID = x.UserID,
                                   Email = x.Email,
                                   Password = x.Password,
                                   ConfirmedEmail = x.ConfirmedEmail,
                                   RoleID = x.RoleID,
                                   Name = x.Name,
                                   UserInfoID = x.UserInfoID,
                                   Street = x.Street,
                                   PostalCode = x.PostalCode,
                                   City = x.City,
                                   Phone = x.Phone,
                                   AlternativePhone = x.AlternativePhone,
                                   AlternativeEmail = x.AlternativeEmail,
                                   Facebook = x.Facebook,
                                   Twitter = x.Twitter,
                                   Instagram = x.Instagram,
                                   Tumblr = x.Tumblr,
                                   Website = x.Website
                               }).ToList();
            GetLogin();
            return View(userProfile);
        }

        // Backoffice - GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("ErrorNotFoundOrSomeOtherError");
            }

            var userProfile = (from user in _context.Users
                               join userInfo in _context.UsersInfo on user.UserID equals userInfo.UserID
                               where user.UserID == id
                               select new
                               {
                                   user.UserID,
                                   user.Email,
                                   user.Password,
                                   user.Name,
                                   user.ConfirmedEmail,
                                   user.RoleID,
                                   userInfo.UserInfoID,
                                   userInfo.Street,
                                   userInfo.PostalCode,
                                   userInfo.City,
                                   userInfo.Phone,
                                   userInfo.AlternativePhone,
                                   userInfo.AlternativeEmail,
                                   userInfo.Facebook,
                                   userInfo.Twitter,
                                   userInfo.Instagram,
                                   userInfo.Tumblr,
                                   userInfo.Website
                               }).AsEnumerable().Select(x => new Profile
                               {
                                   UserID = x.UserID,
                                   Email = x.Email,
                                   Password = x.Password,
                                   ConfirmedEmail = x.ConfirmedEmail,
                                   RoleID = x.RoleID,
                                   Name = x.Name,
                                   UserInfoID = x.UserInfoID,
                                   Street = x.Street,
                                   PostalCode = x.PostalCode,
                                   City = x.City,
                                   Phone = x.Phone,
                                   AlternativePhone = x.AlternativePhone,
                                   AlternativeEmail = x.AlternativeEmail,
                                   Facebook = x.Facebook,
                                   Twitter = x.Twitter,
                                   Instagram = x.Instagram,
                                   Tumblr = x.Tumblr,
                                   Website = x.Website
                               }).First(); ;
            if (userProfile == null)
            {
                return RedirectToAction("ErrorNotFoundOrSomeOtherError");
            }
            GetLogin();
            return View(userProfile);
        }

        //Backoffice - GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        //Frontoffice - POST: Users/Create
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

                    int user_id = (from user in _context.Users select user.UserID).Max();
                    //SendSimpleMessage();
                    //string link = String.Format("<h3><a href=\"https://localhost:44359/Users/ConfirmEmail/{0}\">Click here to confirm your account so you can login with it!</a></h3>", user_id);
                    var result = new MailSenderController(_configuration).PostMessage();
                    string link = String.Format("<h3><a href=\"https://eswshelter.azurewebsites.net/Users/ConfirmEmail/{0}\">Click here to confirm your account so you can login with it!</a></h3>", user_id);
                    string subj = "Welcome to our Shelter " + users.Name + "!";
                    string content = "<h1>We, ESW Group 2 Welcome you to our project!</h1>" +
                        "<p><h2>Please, to continue with your registration, we ask that you verify your account in the following link:</h2></p>" +
                        link +
                        "<p><h2>Any questions can be sent to this same email. I hope you enjoy the experience</h2></p>";
                    //await sender.PostMessage(subj, content, users.Email, users.Name);
                    /** End of Confirmation Email **/
                    TempData["Message"] = "Your account has been created!Please check your email and click on the link to confirm your email before trying to login!";
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

        [HttpGet]
        public ActionResult Login()
        {
            return View("~/Views/Home/Index.cshtml");
        }
        //Frontoffice - Post Login
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
                        TempData["Message"] = "This email has not been confirmed yet! Please check your email!";
                        return View("~/Views/Home/Index.cshtml");
                    }
                    LoginSV(userRetrieved.Name, userRetrieved.UserID.ToString());
                    TempData["Message"] = "Login sucessfull!";

                    return RedirectToAction("Login");
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
        //Frontoffice - GET: Users/ConfirmEmail/5
        public async Task<IActionResult> ConfirmEmail(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("ErrorNotFoundOrSomeOtherError");
            }

            Users users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return RedirectToAction("ErrorNotFoundOrSomeOtherError");
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

        public async Task<IActionResult> Logout(int? id)
        {
            var users = await _context.Users.FindAsync(id);
            LoginSV("","");
            TempData["Message"] = "Logout sucessfull! Come Back Soon!";
            return View("~/Views/Home/Index.cshtml");
        }

        public async Task<IActionResult> Profile(int? id)
        {
            if (id == null || HttpContext.Session.GetString("User_Name").Equals("") || HttpContext.Session.GetString("UserID").Equals(""))
            {
                return RedirectToAction("ErrorNotFoundOrSomeOtherError");
            }
            var userProfile = (from user in _context.Users
                               join userInfo in _context.UsersInfo on user.UserID equals userInfo.UserID
                               where user.UserID == id
                               select new
                               {
                                   user.UserID,
                                   user.Email,
                                   user.Password,
                                   user.Name,
                                   user.ConfirmedEmail,
                                   user.RoleID,
                                   userInfo.UserInfoID,
                                   userInfo.Street,
                                   userInfo.PostalCode,
                                   userInfo.City,
                                   userInfo.Phone,
                                   userInfo.AlternativePhone,
                                   userInfo.AlternativeEmail,
                                   userInfo.Facebook,
                                   userInfo.Twitter,
                                   userInfo.Instagram,
                                   userInfo.Tumblr,
                                   userInfo.Website
                               }).First();
            if (userProfile == null)
            {
                return RedirectToAction("ErrorNotFoundOrSomeOtherError");
            }
            Profile profile = new Profile()
            {
                UserID = userProfile.UserID,
                Email = userProfile.Email,
                Password = userProfile.Password,
                ConfirmedEmail = userProfile.ConfirmedEmail,
                RoleID = userProfile.RoleID,
                Name = userProfile.Name,
                UserInfoID = userProfile.UserInfoID,
                Street = userProfile.Street,
                PostalCode = userProfile.PostalCode,
                City = userProfile.City,
                Phone = userProfile.Phone,
                AlternativePhone = userProfile.AlternativePhone,
                AlternativeEmail = userProfile.AlternativeEmail,
                Facebook = userProfile.Facebook,
                Twitter = userProfile.Twitter,
                Instagram = userProfile.Instagram,
                Tumblr = userProfile.Tumblr,
                Website = userProfile.Website
            };
                GetLogin();
                return View("~/Views/Home/Profile.cshtml", profile);
        }

        /***************** Unchecked if correct **************************************************/

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || !GetLogin())
            {
                return RedirectToAction("ErrorNotFoundOrSomeOtherError");
            }
            var userProfile = (from user in _context.Users
                               join userInfo in _context.UsersInfo on user.UserID equals userInfo.UserID
                               where user.UserID == id
                               select new
                               {
                                   user.UserID,
                                   user.Email,
                                   user.Password,
                                   user.Name,
                                   user.ConfirmedEmail,
                                   user.RoleID,
                                   userInfo.UserInfoID,
                                   userInfo.Street,
                                   userInfo.PostalCode,
                                   userInfo.City,
                                   userInfo.Phone,
                                   userInfo.AlternativePhone,
                                   userInfo.AlternativeEmail,
                                   userInfo.Facebook,
                                   userInfo.Twitter,
                                   userInfo.Instagram,
                                   userInfo.Tumblr,
                                   userInfo.Website
                               }).First();
            if (userProfile == null)
            {
                return RedirectToAction("ErrorNotFoundOrSomeOtherError");
            }
            Profile profile = new Profile()
            {
                UserID = userProfile.UserID,
                Email = userProfile.Email,
                Password = userProfile.Password,
                ConfirmedEmail = userProfile.ConfirmedEmail,
                RoleID = userProfile.RoleID,
                Name = userProfile.Name,
                UserInfoID = userProfile.UserInfoID,
                Street = userProfile.Street,
                PostalCode = userProfile.PostalCode,
                City = userProfile.City,
                Phone = userProfile.Phone,
                AlternativePhone = userProfile.AlternativePhone,
                AlternativeEmail = userProfile.AlternativeEmail,
                Facebook = userProfile.Facebook,
                Twitter = userProfile.Twitter,
                Instagram = userProfile.Instagram,
                Tumblr = userProfile.Tumblr,
                Website = userProfile.Website
            };
            if (!GetLogin())
            {
                GetLogin();
                return View("~/Views/Home/Profile.cshtml", profile);
            } else if (GetLogin())
            {
                return View("Edit", profile);
            }
            return View("~/Views/Home/Index.cshtml");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserID, Email, Password, Name, ConfirmedEmail, RoleID, UserInfoID, Street, PostalCode, City, Phone, AlternativePhone, AlternativeEmail, Facebook, Twitter, Instagram, Tumblr, Website")] Profile profile)
        {
            if (id != profile.UserID)
            {
                return RedirectToAction("ErrorNotFoundOrSomeOtherError");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    Users updateUser = new Users()
                    {
                        UserID = profile.UserID,
                        Email = profile.Email,
                        Name = profile.Name,
                        Password = profile.Password,
                        ConfirmedEmail = profile.ConfirmedEmail,
                        RoleID = profile.RoleID
                    };
                    _context.Users.Update(updateUser);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(profile.UserID))
                    {
                        return RedirectToAction("ErrorNotFoundOrSomeOtherError");
                    }
                    else
                    {
                        throw;
                    }
                }
                // Update UsersInfo table
                try
                {
                    UsersInfo updateUserInfo = new UsersInfo()
                    {
                        UserInfoID = profile.UserInfoID,
                        Street = profile.Street,
                        PostalCode = profile.PostalCode,
                        City = profile.City,
                        Phone = profile.Phone,
                        AlternativePhone = profile.AlternativePhone,
                        AlternativeEmail = profile.AlternativeEmail,
                        Facebook = profile.Facebook,
                        Twitter = profile.Twitter,
                        Instagram = profile.Instagram,
                        Tumblr = profile.Tumblr,
                        Website = profile.Website,
                        UserID = profile.UserID
                    };
                    _context.UsersInfo.Update(updateUserInfo);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersInfoExists(profile.UserInfoID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                await _context.SaveChangesAsync();
                TempData["Message"] = "Profile updated sucessfully!";
                return RedirectToAction("Edit", "Users", new {id = profile.UserID});
            }
            return View("~/Views/Home/Index.cshtml");
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("ErrorNotFoundOrSomeOtherError");
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (users == null)
            {
                return RedirectToAction("ErrorNotFoundOrSomeOtherError");
            }
            GetLogin();
            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userInfo = (from ui in _context.UsersInfo
                            where ui.UserID == id
                            select ui).First();
            _context.UsersInfo.Remove(userInfo);

            var users = await _context.Users.FindAsync(id);
            _context.Users.Remove(users);

            await _context.SaveChangesAsync();
            GetLogin();
            return RedirectToAction(nameof(Index));
        }

        //Goes here if anything goes wrong
        public async Task<IActionResult> ErrorNotFoundOrSomeOtherError()
        {
            TempData["Message"] = "Access Denied";
            return View("~/Views/Home/Index.cshtml");
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.UserID == id);
        }

        private bool UsersInfoExists(int id)
        {
            return _context.UsersInfo.Any(e => e.UserInfoID == id);
        }

        private void LoginSV(String name, String id)
        {
            if(name.Equals(""))
            {
                HttpContext.Session.Remove("User_Name");
                HttpContext.Session.Remove("UserID");
            } else
            {
                HttpContext.Session.SetString("User_Name", name);
                HttpContext.Session.SetString("UserID", id);
                int idint = Int32.Parse(id);
                var role = (from user in _context.Users where user.UserID == idint select user.RoleID).First();
                if (role == 4)
                {
                    HttpContext.Session.SetString("Ad", "Ad");
                }
            }
        }

        private bool GetLogin()
        {
            if (HttpContext.Session.GetString("User_Name") != null && HttpContext.Session.GetString("UserID") != null)
            {
                HttpContext.Session.SetString("User_Name", HttpContext.Session.GetString("User_Name"));
                HttpContext.Session.SetString("UserID", HttpContext.Session.GetString("UserID"));
            }
            if (HttpContext.Session.GetString("Ad") != null)
            {
                HttpContext.Session.SetString("Ad", HttpContext.Session.GetString("Ad"));
                return true;
            }
            return false;
        }
    }

    internal class UserProfile
    {
    }
}
