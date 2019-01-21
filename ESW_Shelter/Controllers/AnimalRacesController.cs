using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ESW_Shelter.Models;

namespace ESW_Shelter.Controllers
{
    public class AnimalRacesController : Controller
    {
        private readonly ShelterContext _context;

        public AnimalRacesController(ShelterContext context)
        {
            _context = context;
        }

        // GET: AnimalRaces
        public async Task<IActionResult> Index()
        {
            return View(await _context.AnimalRace.ToListAsync());
        }

        // GET: AnimalRaces/Details/5
        public async Task<IActionResult> Details(int? id)
        {
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
            return View();
        }

        // POST: AnimalRaces/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnimalRaceID,Name")] AnimalRace animalRace)
        {
            if (ModelState.IsValid)
            {
                _context.Add(animalRace);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(animalRace);
        }

        // GET: AnimalRaces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
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
            if (id != animalRace.AnimalRaceID)
            {
                return NotFound();
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
                return RedirectToAction(nameof(Index));
            }
            return View(animalRace);
        }

        // GET: AnimalRaces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
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
            var animalRace = await _context.AnimalRace.FindAsync(id);
            _context.AnimalRace.Remove(animalRace);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalRaceExists(int id)
        {
            return _context.AnimalRace.Any(e => e.AnimalRaceID == id);
        }
    }
}
