﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ESW_Shelter.Models;

namespace ESW_Shelter.Controllers
{
    public class AnimalRacesController : SharedController
    {
        private readonly ShelterContext _context;

        public AnimalRacesController(ShelterContext context) : base (context)
        {
            _context = context;
        }

        // GET: AnimalRaces
        public async Task<IActionResult> Index()
        {
            if(!GetAuthorization(7, 'r'))
            {
                return NotFound();
            }
            ViewBag.Permission = getPermissions();
            return View(await _context.AnimalRace.ToListAsync());
        }

        // GET: AnimalRaces/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!GetAuthorization(7, 'r'))
            {
                return NotFound();
            }
            ViewBag.Permission = getPermissions();
            if (id == null)
            {
                return NotFound();
            }

            var animalRace = await _context.AnimalRace
                .FirstOrDefaultAsync(m => m.AnimalRaceID == id);
            if (animalRace == null)
            {
                return NotFound();
            }

            return View(animalRace);
        }

        // GET: AnimalRaces/Create
        public IActionResult Create()
        {
            if (!GetAuthorization(7, 'c'))
            {
                return NotFound();
            }
            ViewBag.Permission = getPermissions();
            return View();
        }

        // POST: AnimalRaces/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnimalRaceID,Name")] AnimalRace animalRace)
        {
            if (!GetAuthorization(7, 'c'))
            {
                return NotFound();
            }
            ViewBag.Permission = getPermissions();
            if (!checkValues(animalRace))
            {
                return View(animalRace);
            }
            if (ModelState.IsValid)
            {
                _context.Add(animalRace);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Raça de animal criado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            return View(animalRace);
        }

        // GET: AnimalRaces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!GetAuthorization(7, 'u'))
            {
                return NotFound();
            }
            ViewBag.Permission = getPermissions();
            if (id == null)
            {
                return NotFound();
            }

            var animalRace = await _context.AnimalRace.FindAsync(id);
            if (animalRace == null)
            {
                return NotFound();
            }
            return View(animalRace);
        }

        // POST: AnimalRaces/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnimalRaceID,Name")] AnimalRace animalRace)
        {
            if (!GetAuthorization(7, 'u'))
            {
                return NotFound();
            }
            ViewBag.Permission = getPermissions();
            if (id != animalRace.AnimalRaceID)
            {
                return NotFound();
            }
            if (!checkValues(animalRace))
            {
                return View(animalRace);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animalRace);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalRaceExists(animalRace.AnimalRaceID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["Message"] = "Raça de animal editada com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            return View(animalRace);
        }

        // GET: AnimalRaces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!GetAuthorization(7, 'd'))
            {
                return NotFound();
            }
            ViewBag.Permission = getPermissions();
            if (id == null)
            {
                return NotFound();
            }

            var animalRace = await _context.AnimalRace
                .FirstOrDefaultAsync(m => m.AnimalRaceID == id);
            if (animalRace == null)
            {
                return NotFound();
            }

            return View(animalRace);
        }

        // POST: AnimalRaces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!GetAuthorization(7, 'd'))
            {
                return NotFound();
            }
            ViewBag.Permission = getPermissions();
            var check = _context.Animal.Where(e => e.AnimalRaceFK == id);
            if (check.Any())
            {
                TempData["Message"] = "Raça de animal que pretende eliminar têm animais associados a ela! Por favor altere primeiro esses animais e depois tente eliminar novamente!";
                return RedirectToAction(nameof(Index));
            }
            var animalRace = await _context.AnimalRace.FindAsync(id);
            _context.AnimalRace.Remove(animalRace);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Raça de animal eliminada com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalRaceExists(int id)
        {
            return _context.AnimalRace.Any(e => e.AnimalRaceID == id);
        }


        private bool checkValues(AnimalRace animalRace)
        {
            if (string.IsNullOrEmpty(animalRace.Name))
            {
                TempData["Message"] = "Por favor insira um nome para a raça!";
                return false;
            }
            if (animalRace.AnimalRaceID <= -1)
            {
                TempData["Message"] = "Algo de errado aconteceu!";
                return false;
            }
            return true;
        }
    }
}
