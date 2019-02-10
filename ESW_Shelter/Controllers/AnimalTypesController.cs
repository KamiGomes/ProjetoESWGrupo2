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
    public class AnimalTypesController : SharedController
    {
        private readonly ShelterContext _context;

        public AnimalTypesController(ShelterContext context) : base(context)
        {
            _context = context;
        }

        // GET: AnimalTypes
        public async Task<IActionResult> Index()
        {
            if (!GetAuthorization(6, 'r'))
            {
                return NotFound();
            }
            ViewBag.Permission = getPermissions();
            return View(await _context.AnimalTypes.ToListAsync());
        }
        
        // GET: AnimalTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!GetAuthorization(6, 'r'))
            {
                return NotFound();
            }
            ViewBag.Permission = getPermissions();
            if (id == null)
            {
                return NotFound();
            }

            var animalType = await _context.AnimalTypes
                .FirstOrDefaultAsync(m => m.AnimalTypeID == id);
            if (animalType == null)
            {
                return NotFound();
            }

            return View(animalType);
        }

        // GET: AnimalTypes/Create
        public async Task<IActionResult> Create()
        {
            if (!GetAuthorization(6, 'c'))
            {
                return NotFound();
            }
            ViewBag.Permission = getPermissions();
            return View();
        }

        // POST: AnimalTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnimalTypeID,Name")] AnimalType animalType)
        {
            if (!GetAuthorization(6, 'c'))
            {
                return NotFound();
            }
            ViewBag.Permission = getPermissions();
            if (!checkValues(animalType))
            {
                return View(animalType);
            }
            if (ModelState.IsValid)
            {
                _context.Add(animalType);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Tipo de animal criado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            return View(animalType);
        }

        // GET: AnimalTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!GetAuthorization(6, 'u'))
            {
                return NotFound();
            }
            ViewBag.Permission = getPermissions();
            if (id == null)
            {
                return NotFound();
            }

            var animalType = await _context.AnimalTypes.FindAsync(id);
            if (animalType == null)
            {
                return NotFound();
            }
            return View(animalType);
        }

        // POST: AnimalTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnimalTypeID,Name")] AnimalType animalType)
        {
            if (!GetAuthorization(6, 'u'))
            {
                return NotFound();
            }
            ViewBag.Permission = getPermissions();
            if (!checkValues(animalType))
            {
                return View(animalType);
            }
            if (id != animalType.AnimalTypeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animalType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalTypeExists(animalType.AnimalTypeID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["Message"] = "Tipo de animal editado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            return View(animalType);
        }

        // GET: AnimalTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!GetAuthorization(6, 'd'))
            {
                return NotFound();
            }
            ViewBag.Permission = getPermissions();
            if (id == null)
            {
                return NotFound();
            }

            var animalType = await _context.AnimalTypes
                .FirstOrDefaultAsync(m => m.AnimalTypeID == id);
            if (animalType == null)
            {
                return NotFound();
            }

            return View(animalType);
        }

        // POST: AnimalTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!GetAuthorization(6, 'd'))
            {
                return NotFound();
            }
            ViewBag.Permission = getPermissions();
            var check = _context.Animal.Where(e => e.AnimalTypeFK == id);
            var check2 = _context.Products.Where(e => e.AnimalTypeFK == id);
            if (check.Any() || check2.Any())
            {
                TempData["Message"] = "Tipo de animal que pretende eliminar têm animais associados a ela! Por favor altere primeiro (Animais ou Produtos) e depois tente eliminar novamente!";
                return RedirectToAction(nameof(Index));
            }
            var animalType = await _context.AnimalTypes.FindAsync(id);
            _context.AnimalTypes.Remove(animalType);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Tipo de animal eliminado com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalTypeExists(int id)
        {
            return _context.AnimalTypes.Any(e => e.AnimalTypeID == id);
        }

        private bool checkValues(AnimalType animalType)
        {
            if (string.IsNullOrEmpty(animalType.Name))
            {
                TempData["Message"] = "Por favor introduza um nome de tipo de animal!";
                return false;
            }
            if (animalType.AnimalTypeID <= -1)
            {
                TempData["Message"] = "Algo de errado ocorreu!";
                return false;
            }
            return true;
        }
    }
}
