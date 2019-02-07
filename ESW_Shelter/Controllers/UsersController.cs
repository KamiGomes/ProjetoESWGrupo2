using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ESW_Shelter.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Rendering;
using ESW_Shelter.Libs;

//hotfix -> Install-Package Microsoft.AspNet.Mvc -Version 5.2.3.0 | Install-Package httpsecurecookie -Version 0.1.1 | Install-Package Microsoft.AspNetCore.Session -Version 2.1.1 
//Insert Into Users (Email, Name, Password, ConfirmedEmail, RoleId, City, DateOfBirth, Phone, PostalCode, Street) Values
//					('administrador@admin.pt', 'Admin', 'Admin-12',1,4,'Cidade','2018/12/16',000000000,'0000-000','Nenhuma')
namespace ESW_Shelter.Controllers
{

    public class UsersController : SharedController
    {

        private readonly ShelterContext _context;
        private readonly IConfiguration _configuration;


        public UsersController(ShelterContext context, IConfiguration configuration) : base(context)
        {

            _context = context;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index(string searchString, string roleType)
        {
            if (!GetAuthorization(1, 'r'))
            {
                return NotFound();
            }
            ViewBag.Permission = getPermissions();
            var query = from usersJ in _context.Users
                        join rolesJ in _context.Roles on usersJ.RoleID equals rolesJ.RoleID
                        select new
                        {
                            UserID = usersJ.UserID,
                            Name = usersJ.Name,
                            Email = usersJ.Email,
                            ConfirmedEmail = usersJ.ConfirmedEmail,
                            RoleName = rolesJ.RoleName,
                            RoleID = rolesJ.RoleID,
                            CustomerID = usersJ.CustomerId
                        };

            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(user => user.Name.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(roleType))
            {
                query = query.Where(user => user.RoleName.Contains(roleType));
            }

            var result = query.ToList().Select(e => new Users
            {
                UserID = e.UserID,
                Name = e.Name,
                Email = e.Email,
                ConfirmedEmail = e.ConfirmedEmail,
                RoleName = e.RoleName,
                RoleID = e.RoleID,
                CustomerId = e.CustomerID
            }).ToList();

            var rolesTypesQuery = from roles in _context.Roles
                                  orderby roles.RoleName
                                  select roles.RoleName;

            var usersIndexVM = new UsersIndexViewModel
            {
                Users = result,
                RolesType = new SelectList(rolesTypesQuery.Distinct().ToList()),
            };

            Dictionary<int, string> dict = new Dictionary<int, string>();
            StripeLib stripeLib = new StripeLib();
            foreach (Users user in result)
            {
                try
                {
                    dict.Add(user.UserID, stripeLib.GetSubscription(user.CustomerId));
                }
                catch (Exception ex)
                {
                    dict.Add(user.UserID, "N/A");
                }

            }

            ViewBag.Subscriptions = dict;

            return View(usersIndexVM);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (!GetAuthorization(1, 'r'))
            {
                return NotFound();
            }
            ViewBag.Permission = getPermissions();
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
            var query = from usersJ in _context.Users
                        join rolesJ in _context.Roles on usersJ.RoleID equals rolesJ.RoleID
                        where usersJ.UserID == id
                        select new
                        {
                            UserID = usersJ.UserID,
                            Name = usersJ.Name,
                            Email = usersJ.Email,
                            ConfirmedEmail = usersJ.ConfirmedEmail,
                            City = usersJ.City,
                            DateOfBirth = usersJ.DateOfBirth,
                            Phone = usersJ.Phone,
                            PostalCode = usersJ.PostalCode,
                            Street = usersJ.Street,
                            RoleName = rolesJ.RoleName
                        };

            Users result = query.ToList().Select(e => new Users
            {
                UserID = e.UserID,
                Name = e.Name,
                Email = e.Email,
                ConfirmedEmail = e.ConfirmedEmail,
                City = e.City,
                DateOfBirth = e.DateOfBirth,
                Phone = e.Phone,
                PostalCode = e.PostalCode,
                Street = e.Street,
                RoleName = e.RoleName
            }).First();

            return View(result);
        }

        public IActionResult Create()
        {
            if (!GetAuthorization(1, 'c'))
            {

                return NotFound();
            }
            ViewBag.Permission = getPermissions();
            ViewBag.RoleTypes = _context.Roles.AsParallel();
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,Email,Name,Password,ConfirmedEmail,Street,PostalCode,City,Phone,DateOfBirth,RoleID")] Users users)
        {
            if (!GetAuthorization(1, 'c'))
            {
                return NotFound();
            }
            ViewBag.Permission = getPermissions();
            if (!checkValues(users))
            {
                ViewBag.RoleTypes = _context.Roles.AsParallel();
                return View(users);
            }
            if (ModelState.IsValid)
            {
                _context.Add(users);
                await _context.SaveChangesAsync();
                /** Send Confirmation Email **/
                int user_id = (from user in _context.Users select user.UserID).Max();
                var result = new MailSenderController(_configuration).PostMessage(users.Email, users.Name, users.UserID);

                /** End of Confirmation Email **/
                // Register User as a Customer on Stripe
                StripeLib stripeLib = new StripeLib();
                users.CustomerId = await stripeLib.CreateCustomer(users);
                await _context.SaveChangesAsync();

                insertToRegisterTable();
                TempData["Message"] = "Utilizador criado com sucesso!Por favor, o utilizador que verifique o seu email e clique no link para concluir o registo da sua conta e para prosseguir para o login!";
                return RedirectToAction(nameof(Index));
            }
            TempData["Message"] = "Por favor, siga os exemplos para continuar!";
            ViewBag.RoleTypes = _context.Roles.AsParallel();
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("UserID,Email,Name,Password,ConfirmedEmail,Street,PostalCode,City,Phone,DateOfBirth,RoleID")] Users users)
        {
            if (ModelState.IsValid == true)
            {

                if (!_context.Users.Any(x => x.Email == users.Email))
                {
                    /** Password encrypting ***/
                    /*var data = Encoding.ASCII.GetBytes(users.Password);
                    var sha256 = new SHA256CryptoServiceProvider();
                    var sha256data = sha256.ComputeHash(data);
                    users.Password = sha256data;*/
                    /** End of Password encrypting **/
                    users.RoleID = 2;
                    _context.Add(users);

                    await _context.SaveChangesAsync();
                    /** Send Confirmation Email **/

                    int user_id = (from user in _context.Users select user.UserID).Max();
                    var result = new MailSenderController(_configuration).PostMessage(users.Email, users.Name, users.UserID);

                    /** End of Confirmation Email **/
                    // Register User as a Customer on Stripe
                    StripeLib stripeLib = new StripeLib();
                    users.CustomerId = await stripeLib.CreateCustomer(users);
                    await _context.SaveChangesAsync();

                    insertToRegisterTable();

                    TempData["Message"] = "A sua conta foi criada com sucesso!Por favor verifique o seu email e clique no email para concluir o registo da sua conta e prosseguir para o login!";
                    return RedirectToAction("Index", "Home", null);
                }

                ModelState.AddModelError("Email", "Email já existe!");
                return View(users);
            }
            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,Password")] Users users)
        {
            try
            {
                if (users.Email.Equals(""))
                {
                    TempData["Message"] = "Email or Password incorreto!";
                    ModelState.AddModelError("Email", "Email or Password incorreto!");
                    return View("~/Views/Home/Index.cshtml");
                }
                var user = await _context.Users.SingleAsync(i => i.Email == users.Email);

                if (user != null)
                {
                    if (user.ConfirmedEmail == false)
                    {
                        TempData["Message"] = "Este email ainda não foi confirmado! Por favor vá ao seu email e siga as instruções!";
                        return View("~/Views/Home/Index.cshtml");
                    }

                    if (user.Password != users.Password)
                    {
                        TempData["Message"] = "Password Errada!";
                        ModelState.AddModelError("Email", "Email or Password incorreto!");
                        return View("~/Views/Home/Index.cshtml");
                    }

                    LoginSV(user.Name, user.UserID.ToString());
                    TempData["Message"] = "Login efetuado com sucesso!";

                    string date31string = DateTime.Today.ToString("yyyy/MM/dd");
                    DateTime today = DateTime.ParseExact(date31string, "yyyy/MM/dd", null);

                    var checkDate = _context.LoginStatistic.Where(e => e.DateStatistic.Equals(today)).FirstOrDefault();
                    if (checkDate != null)
                    {
                        LoginStatistic update = checkDate;
                        update.Count += 1;
                        _context.LoginStatistic.Update(update);
                    }
                    else
                    {
                        //TimeSpan difference = end - start;
                        LoginStatistic newStatistic = new LoginStatistic
                        {
                            DateStatistic = today,
                            Count = 1
                        };
                        var lastDate = _context.LoginStatistic.LastOrDefault();
                        if (lastDate != null)
                        {
                            TimeSpan difference = today - lastDate.DateStatistic;
                            if (difference.Days != 1)
                            {
                                int daysMissing = difference.Days;
                                while (daysMissing != 1)
                                {
                                    LoginStatistic fillTable = new LoginStatistic
                                    {
                                        DateStatistic = DateTime.ParseExact(DateTime.Today.AddDays(-daysMissing).ToString("yyyy/MM/dd"), "yyyy/MM/dd", null),
                                        Count = 0
                                    };
                                    _context.LoginStatistic.Add(fillTable);
                                    _context.SaveChanges();
                                    --daysMissing;
                                }
                            }
                        }
                        _context.LoginStatistic.Add(newStatistic);
                    }
                    _context.SaveChanges();

                    return RedirectToAction("Index", "Home", null);
                }
                TempData["Message"] = "Email or Password incorreto!";
                ModelState.AddModelError("Email", "Email or Password incorreto!");
                return View("~/Views/Home/Index.cshtml");
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Email or Password incorreto!";
                ModelState.AddModelError("Email", "Email or Password incorreto!");
                return View("~/Views/Home/Index.cshtml");
            }


        }

        public async Task<IActionResult> ConfirmEmail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Users users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            users.ConfirmedEmail = true;
            _context.Update(users);
            await _context.SaveChangesAsync();
            TempData["Message"] = "A sua conta foi ativada! Pode proseguir para o login!";
            return View("~/Views/Home/Index.cshtml");
        }

        public async Task<IActionResult> Logout(int? id)
        {
            if (id != null)
            {
                var users = await _context.Users.FindAsync(id);
                if (users != null)
                {
                    LoginSV("", "");
                    TempData["Message"] = "Logout efetuado com sucesso! Obrigado pela visita e volte brevemente!";
                    return View("~/Views/Home/Index.cshtml");
                }
                else
                {
                    return NotFound();
                }
            }
            return NotFound();
        }

        public async Task<IActionResult> Profile(int? id)
        {
            if (id == null || HttpContext.Session.GetString("User_Name").Equals("") || HttpContext.Session.GetString("UserID").Equals(""))
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            string date31string = user.DateOfBirth.ToString("yyyy/MM/dd");
            user.DateOfBirth = DateTime.ParseExact(date31string, "yyyy/MM/dd", null);

            try
            {
                StripeLib stripeLib = new StripeLib();
                ViewBag.subscription = stripeLib.GetSubscription(user.CustomerId);
            }
            catch (Exception ex)
            {
                ViewBag.subscription = "N/A";
            }

            return View(user);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.Permission = getPermissions();
            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            ViewBag.RoleTypes = _context.Roles.AsParallel();
            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(int id, [Bind("UserID,Email,Name,Password,ConfirmedEmail,Street,PostalCode,City,Phone,DateOfBirth,RoleID")] Users users)
        {
            try
            {
                if (id != users.UserID)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Perfil atualizado com sucesso!";
                    return RedirectToAction("Profile", "Users", new { id = users.UserID });
                }
                TempData["Message"] = "Por favor, siga os exemplos para continuar!";
                ModelState.AddModelError("PostalCode", "Código Postal no Formato Errado!");
                return RedirectToAction("Profile", "Users", new { id = users.UserID });
            }
            catch (NullReferenceException e)
            {
                TempData["Message"] = "Por favor, siga os exemplos para continuar!";
                ModelState.AddModelError("PostalCode", "Código Postal no Formato Errado!");
                return RedirectToAction("Profile", "Users", new { id = users.UserID });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserID,Email,Name,Password,ConfirmedEmail,Street,PostalCode,City,Phone,DateOfBirth,RoleID")] Users user)
        {
            if (!GetAuthorization(1, 'u'))
            {
                return NotFound();
            }
            ViewBag.Permission = getPermissions();
            if (id != user.UserID)
            {
                return NotFound();
            }
            if (!checkValues(user))
            {
                ViewBag.RoleTypes = _context.Roles.AsParallel();
                return View(user);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Users.Update(user);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(user.UserID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                await _context.SaveChangesAsync();
                TempData["Message"] = "Utilizador editado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.RoleTypes = _context.Roles.AsParallel();
            return View(user);
        }

        /// <summary>
        /// <para> Método que vai ser chamado na rota "/Users/Delete/X". Vai receber um id de um Users, verifica a existência dele, e caso exista, retorna a view com o Users encontrado.</para>
        /// <para> Caso o id seja null ou não se encontre um Users, retorna-se <seealso cref="ErrorNotFoundOrSomeOtherError"/>.</para>
        /// </summary>
        /// <param name="id"> Id de Users a ser eleminado.</param>
        /// <permission cref="Administradores">Administradores do sistema.</permission>
        /// <returns>
        /// <para> Caso exista algum erro - <seealso cref="ErrorNotFoundOrSomeOtherError"/></para>
        /// <para> Caso esteja tudo bem - View(users)</para>
        /// </returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (!GetAuthorization(1, 'd'))
            {
                return NotFound();
            }
            ViewBag.Permission = getPermissions();
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!GetAuthorization(1, 'd'))
            {
                return NotFound();
            }
            ViewBag.Permission = getPermissions();
            var result = _context.AnimalUsers.Where(e => e.UsersFK == id);
            if (result.Any())
            {
                foreach (AnimalUsers aniUs in result.ToList())
                {
                    _context.AnimalUsers.Remove(aniUs);
                    _context.SaveChanges();
                }
            }
            var result2 = _context.Donation.Where(e => e.UsersFK == id);
            if (result2.Any())
            {
                foreach (Donation don in result2.ToList())
                {
                    don.UsersFK = 1;
                    _context.Update(don);
                    _context.SaveChanges();
                }
            }
            var users = await _context.Users.FindAsync(id);
            _context.Users.Remove(users);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.UserID == id);
        }

        private void LoginSV(String name, String id)
        {
            if (name.Equals(""))
            {
                HttpContext.Session.Remove("User_Name");
                HttpContext.Session.Remove("UserID");
                HttpContext.Session.Remove("Ad");
            }
            else
            {
                HttpContext.Session.SetString("User_Name", name);
                HttpContext.Session.SetString("UserID", id);
                int idint = Int32.Parse(id);
                int role = _context.Users.Find(idint).RoleID;
                RoleAuthorization existAcess = _context.RoleAuthorization.Where(e => e.RoleFK == role).FirstOrDefault();
                if (existAcess != null)
                {
                    HttpContext.Session.SetString("Ad", "Ad");
                }
                else
                {
                    int x = -1;
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

        // GET: Users/Card
        public string Card()
        {

            var userId = Int32.Parse(HttpContext.Session.GetString("UserID"));
            var user = _context.Users.Find(userId);

            var customerId = user.CustomerId;
            StripeLib stripeLib = new StripeLib();

            Stripe.StripeList<Stripe.Card> cards = stripeLib.GetCards(customerId);

            if (cards.Count() == 0) return null;

            return cards.First().Id;
        }

        private void insertToRegisterTable()
        {
            string date31string = DateTime.Today.ToString("yyyy/MM/dd");
            DateTime today = DateTime.ParseExact(date31string, "yyyy/MM/dd", null);

            var checkDate = _context.RegisterStatistics.Where(e => e.DateStatistic.Equals(today)).FirstOrDefault();
            if (checkDate != null)
            {
                RegisterStatistics update = checkDate;
                update.Count += 1;
                _context.RegisterStatistics.Update(update);
            }
            else
            {
                //TimeSpan difference = end - start;
                RegisterStatistics newStatistic = new RegisterStatistics
                {
                    DateStatistic = today,
                    Count = 1
                };
                var lastDate = _context.RegisterStatistics.LastOrDefault();
                if (lastDate != null)
                {
                    TimeSpan difference = today - lastDate.DateStatistic;
                    if (difference.Days != 1)
                    {
                        int daysMissing = difference.Days;
                        while (daysMissing != 1)
                        {
                            RegisterStatistics fillTable = new RegisterStatistics
                            {
                                DateStatistic = DateTime.ParseExact(DateTime.Today.AddDays(-daysMissing).ToString("yyyy/MM/dd"), "yyyy/MM/dd", null),
                                Count = 0
                            };
                            _context.RegisterStatistics.Add(fillTable);
                            _context.SaveChanges();
                            --daysMissing;
                        }
                    }
                }
                _context.RegisterStatistics.Add(newStatistic);
            }
            _context.SaveChanges();
        }

        private bool checkValues(Users user)
        {
            if (string.IsNullOrEmpty(user.Name))
            {
                TempData["Message"] = "Por favor insira um nome!";
                return false;
            }
            if (string.IsNullOrEmpty(user.Email))
            {
                TempData["Message"] = "Por favor insira um email!";
                return false;
            }
            if (string.IsNullOrEmpty(user.Password))
            {
                TempData["Message"] = "Por favor insira uma password!";
                return false;
            }
            if (user.RoleID <= 0)
            {
                TempData["Message"] = "Por favor escolha uma permissão!";
                return false;
            }
            if (user.UserID <= -1)
            {
                TempData["Message"] = "Algo de errado aconteceu!";
                return false;
            }
            return true;
        }

        [HttpPost, ActionName("Card")]
        public string CreateCard(string number, int month, int year, string cvc)
        {
            try
            {
                var userId = Int32.Parse(HttpContext.Session.GetString("UserID"));
                var user = _context.Users.Find(userId);
                var customerId = user.CustomerId;

                StripeLib stripeLib = new StripeLib();
                stripeLib.SetCard(customerId, number, month, year, cvc);
                return "true";

            }
            catch (Exception ex)
            {
                return "false";
            }
        }
    }
}
