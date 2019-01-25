using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ESW_Shelter.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace ESW_Shelter.Controllers
{
    public class AnimalsController : SharedController
    {
        private readonly ShelterContext _context;
        private readonly IHostingEnvironment hostingEnvironment;
        public AnimalsController(ShelterContext context, IHostingEnvironment environment) : base(context)
        {
            _context = context;
            hostingEnvironment = environment;
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
                Animals = result,
                Pictures = _context.Images.AsParallel().ToList()
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
                AnimalTypeFK = e.AnimalTypeFK,
                AnimalRaceFK = e.AnimalRaceFK,
                OwnerFK = e.OwnerFK,
                AnimaltypeName = e.AnimaltypeName,
                AnimalRaceName = e.AnimalRaceName,
                OwnerName = ""
            }).ToList();

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

            ViewBag.GodFathers = _context.Users.Distinct();
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
        public async Task<IActionResult> Create([Bind("AnimalID,Name,DateOfBirth,Disinfection,Neutered,Description,Foto,Picture,AnimalTypeFK,AnimalRaceFK,OwnerFK")] Animal animal)
        {
            if (ModelState.IsValid)
            {

                string selectedProducts = Request.Form["checkProduct"].ToString();
                string[] selectedProductsList = selectedProducts.Split(',');

                string selectedGodfathers = Request.Form["checkGodfather"].ToString();
                string[] selectedGodfatherList = selectedProducts.Split(',');
                
                Animal animalAdd = new Animal()
                {
                    Name = animal.Name,
                    DateOfBirth = animal.DateOfBirth,
                    Disinfection = animal.Disinfection,
                    Neutered = animal.Neutered,
                    Description = animal.Description,
                    AnimalTypeFK = animal.AnimalTypeFK,
                    AnimalRaceFK = animal.AnimalRaceFK,
                    OwnerFK = animal.OwnerFK
                };
                _context.Add(animalAdd);
                _context.SaveChanges();


                var img = animal.Foto;

                //Getting file meta data
                var fileName = Path.GetFileName(animal.Foto.FileName);
                animal.Description = fileName; // PARA APAGAR

                if (animal.Foto != null)
                {
                    System.IO.Directory.CreateDirectory(hostingEnvironment.WebRootPath + "/images/Galeria_"+animalAdd.AnimalID);
                    var uniqueFileName = GetUniqueFileName(animal.Foto.FileName);
                    var uploads = Path.Combine(hostingEnvironment.WebRootPath, "images/Galeria_"+animalAdd.AnimalID);
                    var filePath = Path.Combine(uploads, uniqueFileName);

                    animal.Foto.CopyTo(new FileStream(filePath, FileMode.Create));

                    Images image = new Images()
                    {
                        AnimalFK = animalAdd.AnimalID,
                        Name = animal.Foto.Name,
                        Length = animal.Foto.Length,
                        FileName = uniqueFileName,
                        ContentType = animal.Foto.ContentType,
                        ContentDisposition = animal.Foto.ContentDisposition
                    };
                    _context.Add(image);
                    _context.SaveChanges();
                    //to do : Save uniqueFileName  to your db table   
                }

                if (selectedProductsList[0] != "")
                {
                    foreach (var temp in selectedProductsList)
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

                if (selectedGodfatherList[0] != "")
                {
                    foreach (var temp in selectedGodfatherList)
                    {

                        int godkey = Convert.ToInt32(temp);
                        AnimalUsers aniUsers = new AnimalUsers()
                        {
                            AnimalFK = animalAdd.AnimalID,
                            UsersFK = godkey
                        };
                        _context.Add(aniUsers);
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
            
            var animalfind = await _context.Animal.FindAsync(id);
            if (animalfind == null)
            {
                return NotFound();
            }
            Animal animalToEdit;
            if (animalfind.OwnerFK == 0)
            {
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
                    AnimalTypeFK = e.AnimalTypeFK,
                    AnimalRaceFK = e.AnimalRaceFK,
                    OwnerFK = e.OwnerFK,
                    AnimaltypeName = e.AnimaltypeName,
                    AnimalRaceName = e.AnimalRaceName,
                    OwnerName = ""
                }).ToList();
                animalToEdit = result.First();
            } else
            {
                var query = from animal in _context.Animal
                            join animalType in _context.AnimalTypes on animal.AnimalTypeFK equals animalType.AnimalTypeID
                            join animalRace in _context.AnimalRace on animal.AnimalRaceFK equals animalRace.AnimalRaceID
                            join owner in _context.Users on animal.OwnerFK equals owner.UserID
                            where animal.AnimalID == id
                            select new
                            {
                                AnimalID = animal.AnimalID,
                                Name = animal.Name,
                                DateOfBirth = animal.DateOfBirth,
                                Disinfection = animal.Disinfection,
                                Neutered = animal.Neutered,
                                Description = animal.Description,
                                AnimalTypeFK = animal.AnimalTypeFK,
                                AnimalRaceFK = animal.AnimalRaceFK,
                                OwnerFK = animal.OwnerFK,
                                AnimaltypeName = animalType.Name,
                                AnimalRaceName = animalRace.Name,
                                OwnerName = owner.Name
                                
                            };
                var result = query.Select(e => new Animal
                {
                    AnimalID = e.AnimalID,
                    Name = e.Name,
                    DateOfBirth = e.DateOfBirth,
                    Disinfection = e.Disinfection,
                    Neutered = e.Neutered,
                    Description = e.Description,
                    AnimalTypeFK = e.AnimalTypeFK,
                    AnimalRaceFK = e.AnimalRaceFK,
                    OwnerFK = e.OwnerFK,
                    AnimaltypeName = e.AnimaltypeName,
                    AnimalRaceName = e.AnimalRaceName,
                    OwnerName = e.OwnerName
                });
                animalToEdit = result.First();
            }

            var productsQuery = from product in _context.Products
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

            var productsResult = productsQuery.ToList().Select(e => new Product
            {
                ProductID = e.ProductID,
                Name = e.Name,
                Quantity = e.Quantity,
                ProductTypeName = e.ProductTypeName,
                AnimaltypeName = e.AnimaltypeName
            }).ToList();

           // ViewBag.AnimalProducts = _context.AnimalProduct.Where(e => e.AnimalFK == id).ToList();
           // ViewBag.GodFathers = _context.AnimalUsers.Where(e => e.AnimalFK == id).ToList();
            ViewBag.Products = productsResult;
            ViewBag.AnimalTypeFK = _context.AnimalTypes.Distinct();
            ViewBag.AnimalRaceFK = _context.AnimalRace.Distinct();
            ViewBag.UsersFK = _context.Users.Distinct();

            return View(animalToEdit);
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
                    string selectedProducts = Request.Form["checkProduct"].ToString();
                    string[] selectedProductsList = selectedProducts.Split(',');

                    string selectedGodfathers = Request.Form["checkGodfather"].ToString();
                    string[] selectedGodfatherList = selectedProducts.Split(',');

                    /*Animal animalAdd = new Animal()
                    {
                        Name = animal.Animals[0].Name,
                        DateOfBirth = animal.Animals[0].DateOfBirth,
                        Disinfection = animal.Animals[0].Disinfection,
                        Neutered = animal.Animals[0].Neutered,
                        Description = animal.Animals[0].Description,
                        Picture = animal.Animals[0].Picture,
                        AnimalTypeFK = animal.Animals[0].AnimalTypeFK,
                        AnimalRaceFK = animal.Animals[0].AnimalRaceFK,
                        OwnerFK = animal.Animals[0].OwnerFK
                    };*/
                    _context.Update(animal);
                    _context.SaveChanges();

                    removeAnimalProducts(id);
                    if (selectedProductsList[0] != "")
                    {
                        foreach (var temp in selectedProductsList)
                        {

                            int prodKey = Convert.ToInt32(temp);
                            AnimalProduct aniProd = new AnimalProduct()
                            {
                                AnimalFK = id,
                                ProductFK = prodKey
                            };
                            _context.Add(aniProd);
                            _context.SaveChanges();

                        }
                    }
                    removeGodFathers(id);
                    if (selectedGodfatherList[0] != "")
                    {
                        foreach (var temp in selectedProductsList)
                        {

                            int godkey = Convert.ToInt32(temp);
                            AnimalUsers aniUsers = new AnimalUsers()
                            {
                                AnimalFK = id,
                                UsersFK = godkey
                            };

                            _context.Add(aniUsers);
                            _context.SaveChanges();

                        }
                    }
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

        private void removeGodFathers(int id)
        {
            var godfathers = _context.AnimalUsers.Where(e=>e.AnimalFK == id);

            foreach(AnimalUsers godkey in godfathers.ToList())
            {
                _context.Remove(godkey);
                _context.SaveChanges();
            }
        }

        private void removeAnimalProducts(int id)
        {
            var godfathers = _context.AnimalProduct.Where(e => e.AnimalFK == id);

            foreach (AnimalProduct animalProd in godfathers.ToList())
            {
                _context.Remove(animalProd);
                _context.SaveChanges();
            }
        }

        private byte[] GetByteArrayFromImage(IFormFile file)
        {
            using (var target = new MemoryStream())
            {
                file.CopyTo(target);
                return target.ToArray();
            }
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }
    }
}
