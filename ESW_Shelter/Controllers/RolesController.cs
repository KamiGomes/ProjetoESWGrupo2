using System;
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
            ViewBag.Components = _context.Components.AsParallel();
            ViewBag.Authorizations = _context.RoleAuthorization.Where(e => e.RoleFK == roles.RoleID).ToList();
            return View(roles);
        }

        // GET: Roles/Create
        public IActionResult Create()
        {
            if (!GetAuthorization(2, 'c'))
            {
                return NotFound();
            }
            ViewBag.Components = _context.Components.AsParallel();
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoleID,RoleName")] Roles roles)
        {
            if (!GetAuthorization(2, 'c'))
            {
                return NotFound();
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

                    if(selectedOptionsList[0] != "")
                    {
                        RoleAuthorization rAutho = new RoleAuthorization
                        {
                            ComponentFK = comp.ComponentID,
                            RoleFK = roles.RoleID,
                            Create = false,
                            Read = false,
                            Update = false,
                            Delete = false
                        };

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
                        _context.Add(rAutho);
                        _context.SaveChanges();
                    }
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(roles);
        }

        // GET: Roles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!GetAuthorization(2, 'u'))
            {
                return NotFound();
            }
            if (id == null)
            {
                return NotFound();
            }

            var roles = await _context.Roles.FindAsync(id);
            if (roles == null)
            {
                return NotFound();
            }
            ViewBag.Components = _context.Components.AsParallel();
            ViewBag.Authorizations = _context.RoleAuthorization.Where(e=> e.RoleFK == roles.RoleID).ToList();
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
            if (id != roles.RoleID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roles);
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
                                int crudId = Int32.Parse(val);
                                switch (crudId)
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
                        _context.Update(rAutho);
                        _context.SaveChanges();
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RolesExists(roles.RoleID))
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
            return View(roles);
        }

        // GET: Roles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!GetAuthorization(2, 'd'))
            {
                return NotFound();
            }
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
            var roles = await _context.Roles.FindAsync(id);
            _context.Roles.Remove(roles);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RolesExists(int id)
        {
            return _context.Roles.Any(e => e.RoleID == id);
        }
    }
}
