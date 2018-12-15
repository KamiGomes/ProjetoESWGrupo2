using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ESW_Shelter.Models;
using System.Data.Entity;

namespace ESW_Shelter.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ShelterContext _context;

        public ProductsController(ShelterContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {

            var query = from product in _context.Products
                        join productType in _context.ProductTypes on product.ProductTypeFK equals productType.ProductTypeID
                        join animalType in _context.AnimalTypes on product.AnimalTypeFK equals animalType.AnimalTypeID
                        select new
                        {
                            ProductID = product.ProductID,
                            Name = product.Name,
                            Quantity = product.Quantity,
                            ProductTypeName = productType.Name,
                            AnimaltypeName = animalType.Name
                        };
            var result = query.ToList().Select(e => new Product
            {
                ProductID = e.ProductID,
                Name = e.Name,
                Quantity = e.Quantity,
                ProductTypeName = e.ProductTypeName,
                AnimaltypeName = e.AnimaltypeName
            }).ToList();
            return View(result);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewBag.AnimalsTypes = _context.AnimalTypes.AsParallel();
            ViewBag.ProductType = _context.ProductTypes.AsParallel();
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductID,Name,Quantity,AnimalTypeFK,ProductTypeFK")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.AnimalTypeFK = Int32.Parse(Request.Form["AnimalTypeFK"].ToString());
                product.ProductTypeFK = Int32.Parse(Request.Form["ProductTypeFK"].ToString());
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductID,Name,Quantity,AnimalTypeFK,ProductTypeFK")] Product product)
        {
            if (id != product.ProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductID))
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
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductID == id);
        }
    }
}
