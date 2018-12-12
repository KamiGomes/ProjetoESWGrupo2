using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ESW_Shelter.Models;

namespace ESW_Shelter.Controllers
{
    public class UsersInfoesController : Controller
    {
        private readonly ShelterContext _context;

        public UsersInfoesController(ShelterContext context)
        {
            _context = context;
        }

        // GET: UsersInfoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.UsersInfo.ToListAsync());
        }

        // GET: UsersInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usersInfo = await _context.UsersInfo
                .FirstOrDefaultAsync(m => m.UserInfoID == id);
            if (usersInfo == null)
            {
                return NotFound();
            }

            return View(usersInfo);
        }

        // GET: UsersInfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UsersInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserInfoID,Street,PostalCode,City,Phone,AlternativePhone,AlternativeEmail,Facebook,Twitter,Instagram,Tumblr,Website,UserID")] UsersInfo usersInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usersInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usersInfo);
        }

        // GET: UsersInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usersInfo = await _context.UsersInfo.FindAsync(id);
            if (usersInfo == null)
            {
                return NotFound();
            }
            return View(usersInfo);
        }

        // POST: UsersInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserInfoID,Street,PostalCode,City,Phone,AlternativePhone,AlternativeEmail,Facebook,Twitter,Instagram,Tumblr,Website,UserID")] UsersInfo usersInfo)
        {
            if (id != usersInfo.UserInfoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usersInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersInfoExists(usersInfo.UserInfoID))
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
            return View(usersInfo);
        }

        // GET: UsersInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usersInfo = await _context.UsersInfo
                .FirstOrDefaultAsync(m => m.UserInfoID == id);
            if (usersInfo == null)
            {
                return NotFound();
            }

            return View(usersInfo);
        }

        // POST: UsersInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usersInfo = await _context.UsersInfo.FindAsync(id);
            _context.UsersInfo.Remove(usersInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersInfoExists(int id)
        {
            return _context.UsersInfo.Any(e => e.UserInfoID == id);
        }
    }
}
