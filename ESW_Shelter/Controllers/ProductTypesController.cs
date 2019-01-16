using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ESW_Shelter.Models;

namespace ESW_Shelter.Controllers
{
    public class ProductTypesController : SharedController
    {
        private readonly ShelterContext _context;

        public ProductTypesController(ShelterContext context) : base(context)
        {
            _context = context;
        }

        // GET: ProductTypes
        public async Task<IActionResult> Index()
        {
            if (!GetAutorization(4))
            {
                return ErrorNotFoundOrSomeOtherError();
            }
            return View(await _context.ProductTypes.ToListAsync());
        }

        // GET: ProductTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!GetAutorization(4))
            {
                return ErrorNotFoundOrSomeOtherError();
            }
            if (id == null)
            {
                return NotFound();
            }

            var productType = await _context.ProductTypes
                .FirstOrDefaultAsync(m => m.ProductTypeID == id);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        // GET: ProductTypes/Create
        public IActionResult Create()
        {
            if (!GetAutorization(4))
            {
                return ErrorNotFoundOrSomeOtherError();
            }
            return View();
        }

        // POST: ProductTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductTypeID,Name")] ProductType productType)
        {
            if (!GetAutorization(4))
            {
                return ErrorNotFoundOrSomeOtherError();
            }
            if (!checkValues(productType))
            {
                return View(productType);
            }
            if (ModelState.IsValid)
            {
                _context.Add(productType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productType);
        }

        // GET: ProductTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!GetAutorization(4))
            {
                return ErrorNotFoundOrSomeOtherError();
            }
            if (id == null)
            {
                return NotFound();
            }

            var productType = await _context.ProductTypes.FindAsync(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        // POST: ProductTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductTypeID,Name")] ProductType productType)
        {
            if (!GetAutorization(4))
            {
                return ErrorNotFoundOrSomeOtherError();
            }
            if (!checkValues(productType))
            {
                return View(productType);
            }
            if (id != productType.ProductTypeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductTypeExists(productType.ProductTypeID))
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
            return View(productType);
        }

        // GET: ProductTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!GetAutorization(4))
            {
                return ErrorNotFoundOrSomeOtherError();
            }
            if (id == null)
            {
                return NotFound();
            }

            var productType = await _context.ProductTypes
                .FirstOrDefaultAsync(m => m.ProductTypeID == id);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        // POST: ProductTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!GetAutorization(4))
            {
                return ErrorNotFoundOrSomeOtherError();
            }
            var productType = await _context.ProductTypes.FindAsync(id);
            _context.ProductTypes.Remove(productType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductTypeExists(int id)
        {
            return _context.ProductTypes.Any(e => e.ProductTypeID == id);
        }

        private bool checkValues(ProductType productType)
        {
            if (!string.IsNullOrEmpty(productType.Name))
            {
                TempData["Message"] = "Por favor insira um tipo de produto!";
                return false;
            }
            if (productType.ProductTypeID <= 0)
            {
                TempData["Message"] = "Algo de errado aconteceu!";
                return false;
            }
            return true;
        }
    }
}
