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
    public class AnimalsController : SharedController
    {
        private readonly ShelterContext _context;

        public AnimalsController(ShelterContext context) : base(context)
        {
            _context = context;
        }

        // GET: Animals
        public async Task<IActionResult> Index()
        {
            if (!GetAutorization(4))
            {
                return ErrorNotFoundOrSomeOtherError();
            }
            /**/
            var query = from animal in _context.Animal
                        join animalType in _context.AnimalTypes on animal.AnimalTypeFK equals animalType.AnimalTypeID
                        join animalRace in _context.AnimalRace on animal.AnimalRaceFK equals animalRace.AnimalRaceID
                        select new
                        {
                            AnimalID = animal.AnimalID,
                            Name = animal.Name,
                            DateOfBirth = animal.DateOfBirth,
                            Disinfection = animal.Disinfection,
                            Neutered = animal.Neutered,
                            Description = animal.Description,
                            Picture = "arranjar mais tarde",
                            AnimalTypeFK = animal.AnimalTypeFK,
                            AnimalRaceFK = animal.AnimalRaceFK,
                            OwnerFK = animal.OwnerFK,
                            AnimaltypeName = animalType.Name,
                            AnimalRaceName = animalRace.Name
                        };


            var result = query.ToList().Select(e => new Animal
            {
                AnimalID = e.AnimalID,
                Name = e.Name,
                DateOfBirth = e.DateOfBirth,
                Disinfection = e.Disinfection,
                Neutered = e.Neutered,
                Description = e.Description,
                Picture = "arranjar mais tarde",
                AnimalTypeFK = e.AnimalTypeFK,
                AnimalRaceFK = e.AnimalRaceFK,
                OwnerFK = e.OwnerFK,
                AnimaltypeName = e.AnimaltypeName,
                AnimalRaceName = e.AnimalRaceName,
                OwnerName = ""
            }
            ).ToList();

            System.Diagnostics.Debug.WriteLine("**********************************************************************");
            System.Diagnostics.Debug.WriteLine(result.FirstOrDefault());
            System.Diagnostics.Debug.WriteLine("**********************************************************************");

            var animalIndexVM = new AnimalIndexViewModel
            {
                Animals = result
            };

            /**/
            return View(animalIndexVM);
        }

        // GET: Animals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            /*
            var animal = await _context.Animal
                .FirstOrDefaultAsync(m => m.AnimalID == id);
            if (animal == null)
            {
                return NotFound();
            }*/

            var query = from animal in _context.Animal
                        join animalType in _context.AnimalTypes on animal.AnimalTypeFK equals animalType.AnimalTypeID
                        join animalRace in _context.AnimalRace on animal.AnimalRaceFK equals animalRace.AnimalRaceID
                        where animal.AnimalID == id
                        select new
                        {
                            AnimalID = animal.AnimalID,
                            Name = animal.Name,
                            DateOfBirth = animal.DateOfBirth,
                            Disinfection = animal.Disinfection,
                            Neutered = animal.Neutered,
                            Description = animal.Description,
                            Picture = "arranjar mais tarde",
                            AnimalTypeFK = animal.AnimalTypeFK,
                            AnimalRaceFK = animal.AnimalRaceFK,
                            OwnerFK = animal.OwnerFK,
                            AnimaltypeName = animalType.Name,
                            AnimalRaceName = animalRace.Name
                        };
            var result = query.ToList().Select(e => new Animal
            {
                AnimalID = e.AnimalID,
                Name = e.Name,
                DateOfBirth = e.DateOfBirth,
                Disinfection = e.Disinfection,
                Neutered = e.Neutered,
                Description = e.Description,
                Picture = "arranjar mais tarde",
                AnimalTypeFK = e.AnimalTypeFK,
                AnimalRaceFK = e.AnimalRaceFK,
                OwnerFK = e.OwnerFK,
                AnimaltypeName = e.AnimaltypeName,
                AnimalRaceName = e.AnimalRaceName,
                OwnerName = ""
            }).ToList();

            System.Diagnostics.Debug.WriteLine("**********************************************************************");
            System.Diagnostics.Debug.WriteLine(result.FirstOrDefault());
            System.Diagnostics.Debug.WriteLine("**********************************************************************");

            var animalIndexVM = new AnimalIndexViewModel
            {
                Animals = result
            };

            ViewBag.UsersFK = _context.Users.AsParallel();
            ViewBag.AnimalRaceFK = _context.AnimalRace.AsParallel();
            ViewBag.AnimalTypeFK = _context.AnimalTypes.AsParallel();
            return View(animalIndexVM);
        }

        // GET: Animals/Create
        public IActionResult Create()
        {
            var query = from product in _context.Products
                        join productsType in _context.ProductTypes on product.ProductTypeFK equals productsType.ProductTypeID
                        join animalsType in _context.AnimalTypes on product.AnimalTypeFK equals animalsType.AnimalTypeID
                        select new
                        {
                            ProductID = product.ProductID,
                            Name = product.Name,
                            Quantity = product.Quantity,
                            AnimalTypeFK = product.AnimalTypeFK,
                            ProductTypeFK = product.ProductTypeFK,
                            ProductTypeName = productsType.Name,
                            AnimaltypeName = animalsType.Name
                        };

            var result = query.ToList().Select(e => new Product
            {
                ProductID = e.ProductID,
                Name = e.Name,
                Quantity = e.Quantity,
                ProductTypeName = e.ProductTypeName,
                AnimaltypeName = e.AnimaltypeName
            }).ToList();

            ViewBag.Products = result;
            ViewBag.AnimalTypeFK = _context.AnimalTypes.Distinct();
            ViewBag.AnimalRaceFK = _context.AnimalRace.Distinct();
            ViewBag.UsersFK = _context.Users.Distinct();
            return View();
        }

        // POST: Animals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnimalID,Name,DateOfBirth,Disinfection,Neutered,Description,Picture,AnimalTypeFK,AnimalRaceFK,OwnerFK")] Animal animal)
        {
            if (ModelState.IsValid)
            {

                string selected = Request.Form["checkProduct"].ToString();
                string[] selectedList = selected.Split(',');

                Animal animalAdd = new Animal()
                {
                    Name = animal.Name,
                    DateOfBirth = animal.DateOfBirth,
                    Disinfection = animal.Disinfection,
                    Neutered = animal.Neutered,
                    Description = animal.Description,
                    Picture = animal.Picture,
                    AnimalTypeFK = animal.AnimalTypeFK,
                    AnimalRaceFK = animal.AnimalRaceFK,
                    OwnerFK = animal.OwnerFK
                };
                _context.Add(animalAdd);
                _context.SaveChanges();

                if (selectedList[0] != "")
                {
                    foreach (var temp in selectedList)
                    {

                        int prodKey = Convert.ToInt32(temp);
                        AnimalProduct aniProd = new AnimalProduct()
                        {
                            AnimalFK = animalAdd.AnimalID,
                            ProductFK = prodKey
                        };
                        _context.Add(aniProd);
                        _context.SaveChanges();

                    }
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(animal);
        }

        // GET: Animals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            /*
            var animal = await _context.Animal.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }*/
            var query = from animal in _context.Animal
                        join animalType in _context.AnimalTypes on animal.AnimalTypeFK equals animalType.AnimalTypeID
                        join animalRace in _context.AnimalRace on animal.AnimalRaceFK equals animalRace.AnimalRaceID
                        where animal.AnimalID == id
                        select new
                        {
                            AnimalID = animal.AnimalID,
                            Name = animal.Name,
                            DateOfBirth = animal.DateOfBirth,
                            Disinfection = animal.Disinfection,
                            Neutered = animal.Neutered,
                            Description = animal.Description,
                            Picture = "arranjar mais tarde",
                            AnimalTypeFK = animal.AnimalTypeFK,
                            AnimalRaceFK = animal.AnimalRaceFK,
                            OwnerFK = animal.OwnerFK,
                            AnimaltypeName = animalType.Name,
                            AnimalRaceName = animalRace.Name
                        };
            var result = query.ToList().Select(e => new Animal
            {
                AnimalID = e.AnimalID,
                Name = e.Name,
                DateOfBirth = e.DateOfBirth,
                Disinfection = e.Disinfection,
                Neutered = e.Neutered,
                Description = e.Description,
                Picture = "arranjar mais tarde",
                AnimalTypeFK = e.AnimalTypeFK,
                AnimalRaceFK = e.AnimalRaceFK,
                OwnerFK = e.OwnerFK,
                AnimaltypeName = e.AnimaltypeName,
                AnimalRaceName = e.AnimalRaceName,
                OwnerName = ""
            }).ToList();

            System.Diagnostics.Debug.WriteLine("**********************************************************************");
            System.Diagnostics.Debug.WriteLine(result.FirstOrDefault());
            System.Diagnostics.Debug.WriteLine("**********************************************************************");

            var animalIndexVM = new AnimalIndexViewModel
            {
                Animals = result
            };

            ViewBag.UsersFK = _context.Users.AsParallel();
            ViewBag.AnimalRaceFK = _context.AnimalRace.AsParallel();
            ViewBag.AnimalTypeFK = _context.AnimalTypes.AsParallel();
            return View(animalIndexVM);
        }

        // POST: Animals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnimalID,Name,DateOfBirth,Disinfection,Neutered,Description,Picture,AnimalTypeFK,AnimalRaceFK,OwnerFK")] Animal animal)
        {
            if (id != animal.AnimalID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalExists(animal.AnimalID))
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
            return View(animal);
        }

        // GET: Animals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animal
                .FirstOrDefaultAsync(m => m.AnimalID == id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // POST: Animals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var animal = await _context.Animal.FindAsync(id);
            _context.Animal.Remove(animal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalExists(int id)
        {
            return _context.Animal.Any(e => e.AnimalID == id);
        }
    }
}
