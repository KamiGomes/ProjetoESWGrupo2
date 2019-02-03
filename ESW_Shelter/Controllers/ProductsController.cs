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
    public class ProductsController : SharedController
    {
        private readonly ShelterContext _context;

        public ProductsController(ShelterContext context) : base(context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index(string searchString, string animalType, string productType)
        {
            if (!GetAuthorization(4, 'r'))
            {
                return NotFound();
            }
            var query = from product in _context.Products
                        join productsType in _context.ProductTypes on product.ProductTypeFK equals productsType.ProductTypeID
                        join animalsType in _context.AnimalTypes on product.AnimalTypeFK equals animalsType.AnimalTypeID
                        select new
                        {
                            ProductID = product.ProductID,
                            Name = product.Name,
                            Quantity = product.Quantity,
                            WeekStock = product.WeekStock,
                            MonthStock = product.MonthStock,
                            AnimalTypeFK = product.AnimalTypeFK,
                            ProductTypeFK = product.ProductTypeFK,
                            ProductTypeName = productsType.Name,
                            AnimaltypeName = animalsType.Name
                        };
            
            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(product => product.Name.Contains(searchString));
            }
            
            if (!String.IsNullOrEmpty(animalType))
            {
                query = query.Where(product => product.AnimaltypeName.Contains(animalType));
            }
            
            if (!String.IsNullOrEmpty(productType))
            {
                query = query.Where(product => product.ProductTypeName.Contains(productType));
            }

            var result = query.ToList().Select(e => new Product
            {
                ProductID = e.ProductID,
                Name = e.Name,
                Quantity = e.Quantity,
                WeekStock = e.WeekStock,
                MonthStock = e.MonthStock,
                ProductTypeName = e.ProductTypeName,
                AnimaltypeName = e.AnimaltypeName
            }).ToList();


            var animalTypeQuery = from animal in _context.AnimalTypes
                                            orderby animal.Name
                                            select animal.Name;

            var productTypeQuery = from product in _context.ProductTypes
                                                 orderby product.Name
                                                 select product.Name;

            var productIndexVM = new ProductIndexViewModel
            {
                Products = result,
                AnimalTypes = new SelectList( animalTypeQuery.Distinct().ToList()),
                ProductTypes = new SelectList( productTypeQuery.Distinct().ToList())
            };

            return View(productIndexVM);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!GetAuthorization(4, 'r'))
            {
                return NotFound();
            }
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
            if (!GetAuthorization(4, 'c'))
            {
                return NotFound();
            }
            ViewBag.AnimalsTypes = _context.AnimalTypes.AsParallel();
            ViewBag.ProductType = _context.ProductTypes.AsParallel();
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductID,Name,Quantity,WeekStock,MonthStock,AnimalTypeFK,ProductTypeFK")] Product product)
        {
            if (!GetAuthorization(4, 'c'))
            {
                return NotFound();
            }
            if (!checkValues(product))
            {
                return RedirectToAction(nameof(Create));
            }
            if (ModelState.IsValid)
            {
                product.AnimalTypeFK = Int32.Parse(Request.Form["AnimalTypeFK"].ToString());
                product.ProductTypeFK = Int32.Parse(Request.Form["ProductTypeFK"].ToString());
                _context.Add(product);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Produto criado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!GetAuthorization(4, 'u'))
            {
                return NotFound();
            }
            ViewBag.AnimalsTypes = _context.AnimalTypes.AsParallel();
            ViewBag.ProductType = _context.ProductTypes.AsParallel();

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
        public async Task<IActionResult> Edit(int id, [Bind("ProductID,Name,Quantity,WeekStock,MonthStock,AnimalTypeFK,ProductTypeFK")] Product product)
        {
            if (!GetAuthorization(4, 'u'))
            {
                return NotFound();
            }
            if (!checkValues(product))
            {
                return RedirectToAction(nameof(Create));
            }
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
                TempData["Message"] = "Produto editado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!GetAuthorization(4, 'd'))
            {
                return NotFound();
            }
            if (id == null)
            {
                return NotFound();
            }

            var product = from productF in _context.Products
                        join productsType in _context.ProductTypes on productF.ProductTypeFK equals productsType.ProductTypeID
                        join animalsType in _context.AnimalTypes on productF.AnimalTypeFK equals animalsType.AnimalTypeID
                        where productF.ProductID == id
                        select new
                        {
                            ProductID = productF.ProductID,
                            Name = productF.Name,
                            Quantity = productF.Quantity,
                            AnimalTypeFK = productF.AnimalTypeFK,
                            ProductTypeFK = productF.ProductTypeFK,
                            ProductTypeName = productsType.Name,
                            AnimaltypeName = animalsType.Name
                        };

            if (product == null)
            {
                return NotFound();
            }

            var result = new Product
            {
                ProductID = product.First().ProductID,
                Name = product.First().Name,
                Quantity = product.First().Quantity,
                ProductTypeName = product.First().ProductTypeName,
                AnimaltypeName = product.First().AnimaltypeName
            };

            return View(result);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!GetAuthorization(4, 'd'))
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Produto eliminado com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductID == id);
        }

        private bool checkValues(Product product)
        {
            if (product.AnimalTypeFK <= 0)
            {
                TempData["Message"] = "Por favor insira um tipo de animal!";
                return false;
            }
            if (product.ProductTypeFK <= 0)
            {
                TempData["Message"] = "Por favor escolha um tipo de produto!";
                return false;
            }
            if (string.IsNullOrEmpty(product.Name))
            {
                TempData["Message"] = "Por favor insira um nome para o produto!";
                return false;
            }
            if (product.Quantity <= -1)
            {
                TempData["Message"] = "Quantidade não pode ser negativa!";
                return false;
            }
            if (product.ProductID <= -1)
            {
                TempData["Message"] = "Algo de errado aconteceu!";
                return false;
            }
            return true;
        }
    }
}
