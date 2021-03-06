﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ESW_Shelter.Models;

namespace ESW_Shelter.Controllers
{
    public class RolesController : SharedController
    {
        private readonly ShelterContext _context;

        public RolesController(ShelterContext context) : base(context)
        {
            _context = context;
        }

        // GET: Roles
        public async Task<IActionResult> Index()
        {
            if (!GetAuthorization(2, 'r'))
            {
                return NotFound();
            }
            ViewBag.Permission = getPermissions();
            var roles = await _context.Roles.ToListAsync();
            return View(roles);
        }

        // GET: Roles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!GetAuthorization(2, 'r'))
            {
                return NotFound();
            }
            ViewBag.Permission = getPermissions();
            if (id == null)
            {
                return NotFound();
            }

            var roles = await _context.Roles
                .FirstOrDefaultAsync(m => m.RoleID == id);
            if (roles == null)
            {
                return NotFound();
            }
            setViewBags(roles.RoleID);
            return View(roles);
        }

        // GET: Roles/Create
        public IActionResult Create()
        {
            if (!GetAuthorization(2, 'c'))
            {
                return NotFound();
            }
            ViewBag.Permission = getPermissions();
            setViewBags(-1);
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoleID,RoleName")] Roles roles)
        {
            System.Diagnostics.Debug.WriteLine("******************************************************");
            System.Diagnostics.Debug.WriteLine("Entrou");
            System.Diagnostics.Debug.WriteLine("******************************************************");
            if (!GetAuthorization(2, 'c'))
            {
                return NotFound();
            }
            ViewBag.Permission = getPermissions();
            if (!checkValues(roles))
            {
                setViewBags(-1);
                return View(roles);
            }
            if (ModelState.IsValid)
            {
                _context.Add(roles);
                _context.SaveChanges();
                var componenets = _context.Components.ToList();
                foreach (var comp in componenets)
                {
                    string selectedOptions = Request.Form[comp.Name].ToString();
                    string[] selectedOptionsList = selectedOptions.Split(',');

                    RoleAuthorization rAutho = new RoleAuthorization
                    {
                        ComponentFK = comp.ComponentID,
                        RoleFK = roles.RoleID,
                        Create = false,
                        Read = false,
                        Update = false,
                        Delete = false
                    };

                    if (selectedOptionsList[0] != "")
                    {

                        foreach (var val in selectedOptionsList)
                        {
                            int id = Int32.Parse(val);
                            switch (id)
                            {
                                case 1:
                                    rAutho.Create = true;
                                    break;
                                case 2:
                                    rAutho.Read = true;
                                    break;
                                case 3:
                                    rAutho.Update = true;
                                    break;
                                case 4:
                                    rAutho.Delete = true;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    if(rAutho.Delete || rAutho.Create || rAutho.Update)
                    {
                        rAutho.Read = true;
                    }
                    _context.Add(rAutho);
                    _context.SaveChanges();
                }
                await _context.SaveChangesAsync();
                TempData["Message"] = "Permissão criada com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            TempData["Message"] = "Model de errado aconteceu!";
            setViewBags(-1);
            return View(roles);
        }

        // GET: Roles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!GetAuthorization(2, 'u'))
            {
                return NotFound();
            }
            ViewBag.Permission = getPermissions();
            if (id == null)
            {
                return NotFound();
            }

            var roles = await _context.Roles.FindAsync(id);
            if (roles == null)
            {
                return NotFound();
            }
            setViewBags(roles.RoleID);
            return View(roles);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoleID,RoleName")] Roles roles)
        {
            if (!GetAuthorization(2, 'u'))
            {
                return NotFound();
            }
            ViewBag.Permission = getPermissions();
            if (id != roles.RoleID)
            {
                return NotFound();
            }
            if (!checkValues(roles))
            {
                setViewBags(id);
                return View(roles);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roles);
                    var componenets = _context.Components.ToList();
                    var roleRules = _context.RoleAuthorization.Where(e => e.RoleFK == id);

                    foreach (RoleAuthorization roleAutho in roleRules.ToList())
                    {
                        _context.Remove(roleAutho);
                        _context.SaveChanges();
                    }

                    foreach (var comp in componenets)
                    {
                        string selectedOptions = Request.Form[comp.Name].ToString();
                        string[] selectedOptionsList = selectedOptions.Split(',');

                        RoleAuthorization rAutho = new RoleAuthorization
                        {
                            ComponentFK = comp.ComponentID,
                            RoleFK = roles.RoleID,
                            Create = false,
                            Read = false,
                            Update = false,
                            Delete = false
                        };

                        if (selectedOptionsList[0] != "")
                        {

                            foreach (var val in selectedOptionsList)
                            {
                                int crudID = Int32.Parse(val);
                                switch (crudID)
                                {
                                    case 1:
                                        rAutho.Create = true;
                                        break;
                                    case 2:
                                        rAutho.Read = true;
                                        break;
                                    case 3:
                                        rAutho.Update = true;
                                        break;
                                    case 4:
                                        rAutho.Delete = true;
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                        if (rAutho.Delete || rAutho.Create || rAutho.Update)
                        {
                            rAutho.Read = true;
                        }
                        _context.Add(rAutho);
                        _context.SaveChanges();
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RolesExists(roles.RoleID))
                    {
                        System.Diagnostics.Debug.WriteLine("*************************");
                        System.Diagnostics.Debug.WriteLine("Caught Exception");
                        System.Diagnostics.Debug.WriteLine("*************************");
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["Message"] = "Permissão editada com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            setViewBags(id);
            return View(roles);
        }

        // GET: Roles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!GetAuthorization(2, 'd'))
            {
                return NotFound();
            }
            ViewBag.Permission = getPermissions();
            if (id == null)
            {
                return NotFound();
            }

            var roles = await _context.Roles
                .FirstOrDefaultAsync(m => m.RoleID == id);
            if (roles == null)
            {
                return NotFound();
            }

            return View(roles);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!GetAuthorization(2, 'd'))
            {
                return NotFound();
            }
            ViewBag.Permission = getPermissions();
            var result = _context.Users.Where(e=> e.RoleID == id);
            foreach(Users user in result.ToList())
            {
                user.RoleID = 2;
                _context.Users.Update(user);
                _context.SaveChanges();
            }
            var deleteAutho = _context.RoleAuthorization.Where(e => e.RoleFK == id);
            foreach(RoleAuthorization delAutho in deleteAutho.ToList())
            {
                _context.RoleAuthorization.Remove(delAutho);
                _context.SaveChanges();
            }
            var roles = await _context.Roles.FindAsync(id);
            _context.Roles.Remove(roles);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Permissão eliminada com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        private bool RolesExists(int id)
        {
            return _context.Roles.Any(e => e.RoleID == id);
        }

        private void setViewBags(int id)
        {
            if(id == -1)
            {
                ViewBag.Components = _context.Components.AsParallel();
            } else
            {
                ViewBag.Components = _context.Components.AsParallel();
                ViewBag.Authorizations = _context.RoleAuthorization.Where(e => e.RoleFK == id).ToList();
            }
        }

        private bool checkValues(Roles role)
        {
            if (string.IsNullOrEmpty(role.RoleName))
            {
                TempData["Message"] = "Por favor insira um nome para a permissão!";
                return false;
            }
            if (role.RoleID <= -1)
            {
                TempData["Message"] = "Algo de errado aconteceu!";
                return false;
            }
            return true;
        }
    }
}
