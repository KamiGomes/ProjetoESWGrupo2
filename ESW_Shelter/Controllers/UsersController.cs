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
    /// <summary>
    /// <para>Este controlador trata de todas as funções CRUD que tenham a ver com os Users e UsersInfo e também com o Login e o Logout.</para>
    /// </summary>
    /// <remarks>
    /// <para> Todo os métodos abaixo fazem parte deste controlador </para>
    /// <list type="table">
    /// <listheader>
    ///     <term>Método</term>
    ///     <description>Descrição</description>
    /// </listheader>
    /// <item>
    ///     <term><seealso cref="Index"/></term>
    ///     <description>Método que vai ser chamado ao entrar na rota "/Users". Vai mostrar todos os utilizadores.</description>
    /// </item>
    /// <item>
    ///     <term><seealso cref="Details(int?)"/></term>
    ///     <description>Método que vai ser chamado na rota "/Users/Details/x". Mostra os detalhes de um utilizador.</description>
    /// </item>
    /// <item>
    ///     <term><seealso cref="Create"/></term>
    ///     <description>Método que vai ser chamado na rota "/Users/Create". Vai mostrar o formulário de criação de um novo utilizador.</description>
    /// </item>
    /// <item>
    ///     <term><seealso cref="SucessCreation"/></term>
    ///     <description>Método é chamado quando a crição de uma nova conta de utilizador é efetuada. Vai direcionar o utilizador para a HomePage.</description>
    /// </item>
    /// <item>
    ///     <term><seealso cref="Create(Users)"/></term>
    ///     <description>Método que vai ser chamado na rota "/Users/Create". Vai guardar um Users recebido do formulário.</description>
    /// </item>
    /// <item>
    ///     <term><seealso cref="Login"/></term>
    ///     <description>Método que vai ser chamado na rota "/Users/Login". Vai redireccionar a página para a HomePage.</description>
    /// </item>
    /// <item>
    ///     <term><seealso cref="Login(Users)"/></term>
    ///     <description>Método que vai ser chamado na rota "/Users/Login". Vai receber um Users de um formulário, e verificar se as condições estão certas para dar acesso á conta.</description>
    /// </item>
    /// <item>
    ///     <term><seealso cref="ConfirmEmail(int?)"/></term>
    ///     <description>Método que vai ser chamado na rota "/Users/ConfirmEmail/X". Vai receber um id de um Users, e caso esse id esteja correto, verifica o email desse utilizador.</description>
    /// </item>
    /// <item>
    ///     <term><seealso cref="Logout(int?)"/></term>
    ///     <description>Método que vai ser chamado na rota "/Users/Logout". Vai receber um id de um Users, e caso esse id esteja correto, faz logout da conta dele.</description>
    /// </item>
    /// <item>
    ///     <term><seealso cref="Profile(int?)"/></term>
    ///     <description>Método que vai ser chamado na rota "/Users/Profile/X". Vai receber um id de um Users, e caso esse id esteja correto, mostra a página de perfil desse utilizador.</description>
    /// </item>
    /// <item>
    ///     <term><seealso cref="Edit(int?)"/></term>
    ///     <description>Método que vai ser chamado na rota "/Users/Edit/X". Vai receber um id de um Users, e caso esse id esteja correto, mostra a página de edição de dados desse utilizador (Backend).</description>
    /// </item>
    /// <item>
    ///     <term><seealso cref="Edit(int, Profile)"/></term>
    ///     <description>Método que vai ser chamado na rota "/Users/Edit/X". Vai receber um id de um Users e uma variável Profile, para que os dados desse Users seja alterado.</description>
    /// </item>
    /// <item>
    ///     <term><seealso cref="Delete(int?)"/></term>
    ///     <description>Método que vai ser chamado na rota "/Users/Delete/X". Vai receber um id de um Users para que esse seja eliminado.</description>
    /// </item>
    /// <item>
    ///     <term><seealso cref="DeleteConfirmed(int)"/></term>
    ///     <description>Método que vai ser chamado na rota "/Users/Delete/X". Vai receber um id de um Users para ser eleminado.</description>
    /// </item>
    /// <item>
    ///     <term><seealso cref="ErrorNotFoundOrSomeOtherError"/></term>
    ///     <description>Método que é chamado quando existe algum acesso não desejado, ou algum erro ocorrido no lado do servidor.</description>
    /// </item>
    /// <item>
    ///     <term><seealso cref="UsersExists(int)"/></term>
    ///     <description>Método que vai verificar se existe um Users com o id recebido.</description>
    /// </item>
    /// <item>
    ///     <term><seealso cref="UsersInfoExists(int)"/></term>
    ///     <description>Método que vai verificar se existe um UsersInfo com o id recebido.</description>
    /// </item>
    /// <item>
    ///     <term><seealso cref="LoginSV(string, string)"/></term>
    ///     <description>Método que vai definir as session variables que o login é efetuado.</description>
    /// </item>
    /// <item>
    ///     <term><seealso cref="GetLogin"/></term>
    ///     <description>Método que redefine os session variables, para que não haja perda de dados.</description>
    /// </item>
    /// </list>
    /// </remarks>
    public class UsersController : SharedController
    {

        private readonly ShelterContext _context;
        private readonly IConfiguration _configuration;


        public UsersController(ShelterContext context, IConfiguration configuration) : base(context)
        {
            
            _context = context;
            _configuration = configuration;
        }

        /// <summary>
        /// <para>Método que vai ser chamado ao entrar na rota "/Users". Vai mostrar todos os utilizadores.</para>
        /// <para>O método procura todos os Users e UsersInfo associados através de uma query em LINQ, e ao terminar essa pesquisa, para cada um dos rows, cria 
        /// uma variável Profile, para que seja enviada para a View</para>
        /// </summary>
        /// <permission cref="Administrador">Administradores do sistema.</permission>  
        /// <remarks>
        /// <para><b>Query Utilizada</b></para>
        /// <code>
        /// from user in _context.Users
        /// join userInfo in _context.UsersInfo on user.UserID equals userInfo.UserID
        /// select new {(....)}.AsEnumerable().Select(x => new Profile{(...)}.ToList();
        /// </code>
        /// <para><b>View associada: </b>"-Views/Users/Index.cshtml"</para>
        /// </remarks>
        /// <returns>View(userProfile)</returns>

        public async Task<IActionResult> Index(string searchString, string roleType)
        {
            if (!GetAutorization(4))
            {
                return RedirectToAction("ErrorNotFoundOrSomeOtherError");
            }
            var query = from usersJ in _context.Users
                        join rolesJ in _context.Roles on usersJ.UserID equals rolesJ.RoleID
                        select new
                        {
                            UserID = usersJ.UserID,
                            Name = usersJ.Name,
                            Email = usersJ.Email,
                            ConfirmedEmail = usersJ.ConfirmedEmail,
                            RoleName = rolesJ.RoleName,
                            RoleID = rolesJ.RoleID
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
                RoleID = e.RoleID
            }).ToList();

            var rolesTypesQuery = from roles in _context.Roles
                                  orderby roles.RoleName
                                  select roles.RoleName;

            var usersIndexVM = new UsersIndexViewModel
            {
                Users = result,
                RolesType = new SelectList(rolesTypesQuery.Distinct().ToList()),
            };

            return View(usersIndexVM);
        }


        /// <summary>
        /// <para>Método que vai ser chamado na rota "/Users/Details/x". Utilizando uma query LINQ, vai buscar os dados de um Users e UsersInfo associados, cujo id de Users seja
        /// igual ao id recebido.</para>
        /// <para>Caso existe algum erro (inexistência de id ou mesmo utilizador) o método é redirecionado para o <seealso cref="ErrorNotFoundOrSomeOtherError"/>.
        /// Caso esteja tudo correto, irá ser visualizado todos os dados de um utilizador.</para>
        /// </summary>
        /// <param name="id"> ID de um Users</param>
        /// <permission cref="Administrador">Administradores do sistema.</permission>  
        /// <remarks>
        /// <para><b>Query Utilizada</b></para>
        /// <code>
        ///     (from user in _context.Users
        ///      join userInfo in _context.UsersInfo on user.UserID equals userInfo.UserID
        ///      where user.UserID == id
        ///      select new{...}).AsEnumerable().Select(x => new Profile{}).First();
        /// </code>
        /// <para><b>View associada: </b>"-Views/Users/Details.cshtml"</para>
        /// </remarks>
        /// <returns>
        /// <para> Se o id ou mesmo o Users não existir - <seealso cref="ErrorNotFoundOrSomeOtherError"/></para>
        /// <para> Se sucesso - View(userProfile)</para>
        /// </returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (!GetAutorization(4))
            {
                return RedirectToAction("ErrorNotFoundOrSomeOtherError");
            }
            if (id == null)
            {
                return RedirectToAction("ErrorNotFoundOrSomeOtherError");
            }
            return View();
        }

        /// <summary>
        /// <para> Método que vai ser chamado na rota "/Users/Create". Vai mostrar o formulário de criação de um novo utilizador.</para>
        /// </summary>
        /// <permission cref="Administrador">Administradores do sistema.</permission>  
        /// <remarks>
        /// <para><b>View associada: </b>"-Views/Users/Create.cshtml"</para>
        /// </remarks>
        /// <returns>View();</returns>
        public IActionResult Create()
        {
            if (!GetAutorization(4))
            {
                return RedirectToAction("ErrorNotFoundOrSomeOtherError");
            }
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
            if (!GetAutorization(4))
            {
                return RedirectToAction("ErrorNotFoundOrSomeOtherError");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    users.RoleID = Int32.Parse(Request.Form["RoleFK"].ToString());
                    _context.Add(users);
                    await _context.SaveChangesAsync();

                    int user_id = (from user in _context.Users select user.UserID).Max();
                    //var result = new MailSenderController(_configuration).PostMessage(users.Email, users.Name, users.UserID);

                    /** End of Confirmation Email **/

                    TempData["Message"] = "A conta foi criada com sucesso!Por favor, o utilizador que verifique o seu email e clique no link para concluir o registo da sua conta e prosseguir para o login!";
                    return RedirectToAction(nameof(Index));
                }
                TempData["Message"] = "Por favor, siga os exemplos para continuar!";
                ModelState.AddModelError("PostalCode", "Código Postal no Formato Errado!");
                return View();
            }
            catch (NullReferenceException e)
            {
                TempData["Message"] = "Por favor, siga os exemplos para continuar!";
                ModelState.AddModelError("PostalCode", "Código Postal no Formato Errado!");
                return View();
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        /// <summary>
        /// <para>Método que vai ser chamado na rota "/Users/Create". Vai guardar um Users recebido do formulário. Existe a verificação se o model está correto, isto é,
        /// todos os campos estão preenchidos corretamente com o Model correspondente. </para>
        /// <para>Para além da verificação dos campos, verifica-se também a existência de algum email identico na base de dados.</para>
        /// <para>Cria-se também um UsersInfo, associado ao Id do Users que foi acabado de criar.
        /// Após a inserção destes dados na base de dados é enviado um email para o inscrito para que este verifique a sua conta.</para>
        /// </summary>
        /// <param name="users"> Variável recebida do formulário que foi preenchido na criação de conta. </param>
        /// <permission cref="Clientes">Novos utilizadores ao sistema.</permission>  
        /// <remarks>
        /// <para><b>Criação do Users e UsersInfo</b></para>
        /// <code>
        ///     UsersInfo newUserInfo = new UsersInfo();
        ///     _context.Add(users);
        ///     _context.SaveChanges();
        ///     newUserInfo.UserID = _context.Users.Max(user => users.UserID);
        ///     _context.Add(newUserInfo);
        ///     await _context.SaveChangesAsync();
        /// </code>
        /// <para><b>Envio de Email</b></para>
        /// <code>
        /// Inserir aqui quando terminar os testes
        /// </code>
        /// <para><b>View associada: </b>"-Views/Users/Create.cshtml"</para>
        /// </remarks>
        /// <returns>
        /// <para>Se Email Existir - View("~/Views/Home/Account.cshtml")</para>
        /// <para>Se registo for com sucesso - RedirectToAction("SucessCreation")</para>
        /// <para>Default - return View("~/Views/Home/Index.cshtml")</para>
        /// </returns>
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

                    TempData["Message"] = "A sua conta foi criada com sucesso!Por favor verifique o seu email e clique no email para concluir o registo da sua conta e prosseguir para o login!";
                    return RedirectToAction("Index", "Home", null);
                }

                ModelState.AddModelError("Email", "Email já existe!");
                return View(users);
            }
            return View(users);
        }

        /// <summary>
        /// <para>Método que vai ser chamado na rota "/Users/Login". O método recebe uma variável Users com o Email e Password, onde vai ser utilizado para confirmar o login.</para>
        /// <para>Pesquisa-se a existencia da conta através de uma query LINQ, onde se existir, e se o email estiver confirmado (através do link recebido por email) o utilizador pode entrar.</para>
        /// <para>Caso exista algum erro (Email ou Password errada, ou não existir o email) é enviado uma mensagem de erro.</para>
        /// <para>Caso o email não esteja confirmado, uma mensagem de erro é mostrada.</para>
        /// </summary>
        /// <param name="users"> Variável de Users, com o Email e Password.</param>
        /// <permission cref="Clientes">Novos utilizadores ao sistema.</permission>
        /// <exception cref="InvalidOperationException"></exception>
        /// <remarks>
        /// <para><b>Query utilizada para ir buscar Users</b></para>
        /// <code>
        /// (from user in _context.Users 
        /// where user.Email == users.Email &amp;&amp; user.Password == users.Password 
        /// select user).First();
        /// </code>
        /// </remarks>
        /// <returns>
        /// <para>Se utilizador não existir - View("~/Views/Home/Index.cshtml")</para>
        /// <para>Se a confirmação de email não tiver efetuada - View("~/Views/Home/Index.cshtml")</para>
        /// <para>Se for com sucesso - RedirectToAction("Login")</para>
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,Password")] Users users)
        {
            try
            {
                if (users.Email.Equals(""))
                {
                    System.Diagnostics.Debug.WriteLine("*************************");
                    System.Diagnostics.Debug.WriteLine("Vazio");
                    System.Diagnostics.Debug.WriteLine("*************************");
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
                        System.Diagnostics.Debug.WriteLine("*************************");
                        System.Diagnostics.Debug.WriteLine("PasswordErrada");
                        System.Diagnostics.Debug.WriteLine("*************************");
                        TempData["Message"] = "Password Errada!";
                        ModelState.AddModelError("Email", "Email or Password incorreto!");
                        return View("~/Views/Home/Index.cshtml");
                    }

                    LoginSV(user.Name, user.UserID.ToString());
                    TempData["Message"] = "Login efetuado com sucesso!";

                    return RedirectToAction("Index", "Home", null);
                }
                System.Diagnostics.Debug.WriteLine("*************************");
                System.Diagnostics.Debug.WriteLine("Nnehum User");
                System.Diagnostics.Debug.WriteLine("*************************");
                TempData["Message"] = "Email or Password incorreto!";
                ModelState.AddModelError("Email", "Email or Password incorreto!");
                return View("~/Views/Home/Index.cshtml");
            } catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("*************************");
                System.Diagnostics.Debug.WriteLine("Exception");
                System.Diagnostics.Debug.WriteLine("*************************");
                TempData["Message"] = "Email or Password incorreto!";
                ModelState.AddModelError("Email", "Email or Password incorreto!");
                return View("~/Views/Home/Index.cshtml");
            }


        }

        /// <summary>
        /// <para>Método que vai ser chamado na rota "/Users/ConfirmEmail/X". Vai receber um id de um Users, e com esse id vai pesquisar a existência de um.</para>
        /// <para>Caso o id seja null, ou a pesquisa resolve na inexistência de um Users, devolve <seealso cref="ErrorNotFoundOrSomeOtherError"/>.</para>
        /// <para>Caso o Users exista o email fica confirmado e uma envia uma mensagem para o utilizador.</para>
        /// </summary>
        /// <param name="id"> Id do Users cujo email vai ser verificado</param>
        /// <permission cref="Clientes">Novos utilizadores ao sistema.</permission>
        /// <returns>
        /// <para>Caso exista algum erro - RedirectToAction("ErrorNotFoundOrSomeOtherError")</para>
        /// <para>Se tudo correr bem - View("~/Views/Home/Index.cshtml")</para>
        /// </returns>
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
            TempData["Message"] = "A sua conta foi ativada! Pode proseguir para o login!";
            return View("~/Views/Home/Index.cshtml");
        }

        /// <summary>
        /// <para>Método que vai ser chamado na rota "/Users/Logout". Vai receber um id de um Users e caso esse id seja válido, verifica a existência de um Users
        /// com esse Id.</para> 
        /// <para>Caso o Users exista, utiliza-se o <seealso cref="LoginSV(string, string)"/> para eliminar as variáveis de sessão, e manda-se uma
        /// mensagem de confirmação de logout.</para>
        /// <para>Caso exista algum erro, o id ou o Users seja null, é retornado um <seealso cref="ErrorNotFoundOrSomeOtherError"/></para>
        /// </summary>
        /// <param name="id"> ID de um Users</param>
        /// <permission cref="Clientes">Novos utilizadores ao sistema.</permission>
        /// <returns>
        /// <para> Caso exista algum valor null - <seealso cref="ErrorNotFoundOrSomeOtherError"/></para>
        /// <para> Caso corra tudo bem - View("~/Views/Home/Index.cshtml")</para>
        /// </returns>
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
                    return RedirectToAction("ErrorNotFoundOrSomeOtherError");
                }
            }
            return RedirectToAction("ErrorNotFoundOrSomeOtherError");
        }

        /// <summary>
        /// <para>Método que vai ser chamado na rota "/Users/Profile/X". Vai receber um id de um Users para que esse possa alterar os seus dados.</para>
        /// <para> Caso o id seja null ou as variáveis de sessão sejam null, retorna-se <seealso cref="ErrorNotFoundOrSomeOtherError"/>.</para>
        /// <para> Caso esteja tudo correto, vai buscar o Users com o id recebido, cria uma variável de Profile, e devolve juntamente com a view a ser apresentada.</para>
        /// <para> Caso a busca do Users seja null, devolve o mesmo a cima descrito.</para>
        /// </summary>
        /// <param name="id"> Id do Users</param>
        /// <permission cref="Clientes">Novos utilizadores ao sistema.</permission>
        /// <returns>
        /// <para> Caso exista algum erro - RedirectToAction("ErrorNotFoundOrSomeOtherError")</para>
        /// <para> Caso corra tudo bem - View("~/Views/Home/Profile.cshtml", profile)</para>
        /// </returns>
        public async Task<IActionResult> Profile(int? id)
        {
            if (id == null || HttpContext.Session.GetString("User_Name").Equals("") || HttpContext.Session.GetString("UserID").Equals(""))
            {
                return RedirectToAction("ErrorNotFoundOrSomeOtherError");
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return RedirectToAction("ErrorNotFoundOrSomeOtherError");
            }

            string date31string = user.DateOfBirth.ToString("yyyy/MM/dd");
            user.DateOfBirth = DateTime.ParseExact(date31string, "yyyy/MM/dd", null);
            return View(user);
        }

        /// <summary>
        /// <para>Método que vai ser chamado na rota "/Users/Edit/X". Vai receber um id de um Users para o administrador possa alterar os seus dados.</para>
        /// <para> Caso o id seja null ou as variáveis de sessão sejam null, retorna-se <seealso cref="ErrorNotFoundOrSomeOtherError"/>.</para>
        /// <para> Caso esteja tudo correto, vai buscar o Users com o id recebido, cria uma variável de Profile, e devolve juntamente com a view a ser apresentada.</para>
        /// <para> Caso a busca do Users seja null, devolve o mesmo a cima descrito.</para>
        /// <para> Caso não seja administrador ( Verificação no <seealso cref="GetLogin"/> onde se for false é porque não é) manda o mesmo erro.</para>
        /// </summary>
        /// <param name="id"> Id do Users</param>
        /// <permission cref="Administradores">Administradores do sistema.</permission>
        /// <returns>
        /// <para> Caso exista algum erro - RedirectToAction("ErrorNotFoundOrSomeOtherError")</para>
        /// <para> Caso corra tudo bem - View("Edit", profile)</para>
        /// </returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (!GetAutorization(4))
            {
                return RedirectToAction("ErrorNotFoundOrSomeOtherError");
            }
            if (id == null)
            {
                return RedirectToAction("ErrorNotFoundOrSomeOtherError");
            }
            return View("Edit");

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="profile"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(int id, [Bind("UserID,Email,Name,Password,ConfirmedEmail,Street,PostalCode,City,Phone,DateOfBirth,RoleID")] Users users)
        {
            try
            {
                if (id != users.UserID)
                {
                    return RedirectToAction("ErrorNotFoundOrSomeOtherError");
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

        /// <summary>
        /// <para>Método que vai ser chamado na rota "/Users/Edit/X". Vai receber um id de um Users e uma variável Profile que vem do formulário para edição dos dados.</para>
        /// <para> Caso esteja tudo correto, cria-se variáveis Users e UsersInfo dos modelos e preenche-se com os valores recebidos para que essas variáveis sejam atualizadas com a ajuda do _context.</para>
        /// <para> Caso o id seja null retorna-se <seealso cref="ErrorNotFoundOrSomeOtherError"/>.</para>
        /// <para> Caso o modelo não esteja válido, retorna-se uma mensagem para o utilizador e a mesma view</para>
        /// </summary>
        /// <param name="id"> Id de Users para editar os dados.</param>
        /// <param name="profile"> Variável Profile que contem todos os campos para atualizar</param>
        /// <permission cref="Administradores">Administradores do sistema.</permission>
        /// <remarks>
        /// <para><b>Variável Users</b></para>
        /// <code>
        /// Users updateUser = new Users()
        /// {
        ///     UserID = profile.UserID,
        ///     Email = profile.Email,
        ///     Name = profile.Name,
        ///     Password = profile.Password,
        ///     ConfirmedEmail = profile.ConfirmedEmail,
        ///     RoleID = profile.RoleID
        ///};
        /// </code>
        /// <para><b>Variável UsersInfo</b></para>
        /// <code>
        ///UsersInfo updateUserInfo = new UsersInfo()
        ///{
        ///    UserInfoID = profile.UserInfoID,
        ///    Street = profile.Street,
        ///    PostalCode = profile.PostalCode,
        ///    City = profile.City,
        ///    Phone = profile.Phone,
        ///    AlternativePhone = profile.AlternativePhone,
        ///    AlternativeEmail = profile.AlternativeEmail,
        ///    Facebook = profile.Facebook,
        ///    Twitter = profile.Twitter,
        ///    Instagram = profile.Instagram,
        ///    Tumblr = profile.Tumblr,
        ///    Website = profile.Website,
        ///    UserID = profile.UserID
        ///};
        /// </code>
        /// </remarks>
        /// <returns>
        /// <para> Caso exista algum erro - <seealso cref="ErrorNotFoundOrSomeOtherError"/></para>
        /// <para> Caso tudo corra bem - <seealso cref="Edit(int?)"/></para>
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserID, Email, Password, Name, ConfirmedEmail, RoleID, UserInfoID, Street, PostalCode, City, Phone, AlternativePhone, AlternativeEmail, Facebook, Twitter, Instagram, Tumblr, Website")] Users profile)
        {
            if (!GetAutorization(4))
            {
                return RedirectToAction("ErrorNotFoundOrSomeOtherError");
            }
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

                await _context.SaveChangesAsync();
                TempData["Message"] = "Perfil atualizado com sucesso!";
                return RedirectToAction("Edit", "Users", new { id = profile.UserID });
            }
            return RedirectToAction("Index", "Home", null);
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
            if (!GetAutorization(4))
            {
                return RedirectToAction("ErrorNotFoundOrSomeOtherError");
            }
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
            return View(users);
        }

        /// <summary>
        /// <para>Método que vai ser chamado na rota "/Users/Delete/X". Vai receber um id de um Users para ser eleminado após ter recebido a confirmação pela parte do Administrador.</para>
        /// </summary>
        /// <param name="id"> Id de Users a ser eliminado</param>
        /// <permission cref="Administradores">Administradores do sistema.</permission>
        /// <returns>
        /// RedirectToAction(nameof(Index));
        /// </returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!GetAutorization(4))
            {
                return RedirectToAction("ErrorNotFoundOrSomeOtherError");
            }
            var users = await _context.Users.FindAsync(id);
            _context.Users.Remove(users);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        /// <summary>
        /// <para>Método que vai verificar se existe um Users com o id recebido.</para>
        /// </summary>
        /// <param name="id">Id de Users</param>
        /// <returns>
        /// <para>_context.Users.Any(e => e.UserID == id)</para>
        /// </returns>
        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.UserID == id);
        }

        /// <summary>
        /// <para>Método que vai definir as session variables que o login é efetuado.</para>
        /// <para>Caso o recebido seja vazio, as variáveis de sessão são removidas ( chamado pelo <seealso cref="Logout(int?)"/> )</para>
        /// <para>Verifica o role do utilizador, e caso seja admin, declara-se uma outra vairável de sessão</para>
        /// </summary>
        /// <param name="name"> Nome do utilizador</param>
        /// <param name="id"> Id do utilizador </param>
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
                var role = (from user in _context.Users where user.UserID == idint select user.RoleID).First();
                if (role == 4 || role == 3)
                {
                    HttpContext.Session.SetString("Ad", "Ad");
                }
            }
        }

        /// <summary>
        /// <para>Método que redefine os session variables, para que não haja perda de dados. Verifica ainda se é admin ou não e devolve conforme o resultado.</para>
        /// </summary>
        /// <returns>
        /// <para> Caso a variável de sessão "Ad" não exista - false</para>
        /// <para> Caso a variável de sessão "Ad" exista - true</para>
        /// </returns>
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

        [HttpPost, ActionName("Card")]
        public string CreateCard(string number, int month, int year, string cvc)
        {
            try {
                var userId = Int32.Parse(HttpContext.Session.GetString("UserID"));
                var user = _context.Users.Find(userId);
                var customerId = user.CustomerId;

                StripeLib stripeLib = new StripeLib();
                stripeLib.SetCard(customerId, number, month, year, cvc);
                return "true";

            }catch (Exception ex)
            {
                return "false";
            }
        }
    }
}
