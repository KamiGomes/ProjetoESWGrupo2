using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ESW_Shelter.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        //FrontEnd
        public async Task<IActionResult> Animals(string AnimalRace, string AnimalType, bool Neutered, bool Disinfection, string SearchString)
        {
            return View(getAnimalToList(AnimalRace, AnimalType, Neutered, Disinfection, SearchString));
        }
        // GET: Animals
        public async Task<IActionResult> Index(string AnimalRace, string AnimalType, bool Neutered, bool Disinfection, string SearchString)
        {
            if (!GetAuthorization(8, 'r'))
            {
                return NotFound();
            }
            ViewBag.Permission = getPermissions();
            return View(getAnimalToList(AnimalRace, AnimalType, Neutered, Disinfection, SearchString));
        }

        public async Task<IActionResult> AnimalDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.UsersFK = _context.Users.AsParallel();
            ViewBag.AnimalRaceFK = _context.AnimalRace.AsParallel();
            ViewBag.AnimalTypeFK = _context.AnimalTypes.AsParallel();
            return View(getAnimalDetails(id));
        }

        // GET: Animals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!GetAuthorization(8, 'r'))
            {
                return NotFound();
            }
            ViewBag.Permission = getPermissions();
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.UsersFK = _context.Users.AsParallel();
            ViewBag.AnimalRaceFK = _context.AnimalRace.AsParallel();
            ViewBag.AnimalTypeFK = _context.AnimalTypes.AsParallel();
            return View(getAnimalDetails(id));
        }

        // GET: Animals/Create
        public IActionResult Create()
        {
            if (!GetAuthorization(8, 'c'))
            {
                return NotFound();
            }
            ViewBag.Permission = getPermissions();
            setViewBags(-1);
            return View();
        }

        // POST: Animals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnimalID,Name,DateOfBirth,Disinfection,Neutered,Description,Foto,Picture,AnimalTypeFK,AnimalRaceFK,OwnerFK")] Animal animal)
        {
            if (!GetAuthorization(8, 'c'))
            {
                return NotFound();
            }
            ViewBag.Permission = getPermissions();
            if (!checkValues(animal))
            {
                setViewBags(-1);
                return View(animal);
            }
            if (ModelState.IsValid)
            {

                string selectedProducts = Request.Form["checkProduct"].ToString();
                string[] selectedProductsList = selectedProducts.Split(',');

                string selectedGodfathers = Request.Form["checkGodfather"].ToString();
                string[] selectedGodfatherList = selectedGodfathers.Split(',');
                
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
                    foreach (var key in selectedGodfatherList)
                    {

                        int godkey = Convert.ToInt32(key);
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
                TempData["Message"] = "Animal criado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            setViewBags(-1);
            return View(animal);
        }

        // GET: Animals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!GetAuthorization(8, 'u'))
            {
                return NotFound();
            }
            ViewBag.Permission = getPermissions();
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
                    OwnerName = "",
                    Foto = null
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
                    OwnerName = e.OwnerName,
                    Foto = null
                });
                animalToEdit = result.First();
            }
            setViewBags(Convert.ToInt32(id));
            return View(animalToEdit);
        }

        // POST: Animals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnimalID,Name,DateOfBirth,Disinfection,Neutered,Description,Foto,Picture,AnimalTypeFK,AnimalRaceFK,OwnerFK")] Animal animal)
        {
            if (!GetAuthorization(8, 'u'))
            {
                return NotFound();
            }
            ViewBag.Permission = getPermissions();
            if (id != animal.AnimalID)
            {
                return NotFound();
            }
            if (!checkValues(animal))
            {
                setViewBags(id);
                return View(animal);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    string selectedProducts = Request.Form["checkProduct"].ToString();
                    string[] selectedProductsList = selectedProducts.Split(',');

                    string selectedGodfathers = Request.Form["checkGodfather"].ToString();
                    string[] selectedGodfatherList = selectedGodfathers.Split(',');

                    if (animal.Foto != null)
                    {

                        var result = _context.Images.Where(e => e.AnimalFK == animal.AnimalID);
                        if (result.Any())
                        {
                            Images old = result.First();
                            System.IO.File.Delete(hostingEnvironment.WebRootPath + "/images/Galeria_" + animal.AnimalID + "/" + old.FileName);
                            _context.Images.Remove(old);
                            _context.SaveChanges();
                        }
                        else
                        {
                            System.IO.Directory.CreateDirectory(hostingEnvironment.WebRootPath + "/images/Galeria_" + animal.AnimalID);
                        }

                        var uploads = Path.Combine(hostingEnvironment.WebRootPath, "images/Galeria_" + animal.AnimalID);
                        var uniqueFileName = GetUniqueFileName(animal.Foto.FileName);
                        var filePath = Path.Combine(uploads, uniqueFileName);

                        animal.Foto.CopyTo(new FileStream(filePath, FileMode.Create));

                        Images image = new Images()
                        {
                            AnimalFK = animal.AnimalID,
                            Name = animal.Foto.Name,
                            Length = animal.Foto.Length,
                            FileName = uniqueFileName,
                            ContentType = animal.Foto.ContentType,
                            ContentDisposition = animal.Foto.ContentDisposition
                        };
                        _context.Add(image);
                        _context.SaveChanges();
                    }

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
                        foreach (var temp in selectedGodfatherList)
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
                TempData["Message"] = "Animal editado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            setViewBags(id);
            return View(animal);
        }

        // GET: Animals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!GetAuthorization(8, 'd'))
            {
                return NotFound();
            }
            ViewBag.Permission = getPermissions();
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
            if (!GetAuthorization(8, 'd'))
            {
                return NotFound();
            }
            ViewBag.Permission = getPermissions();
            var result = _context.Images.Where(e => e.AnimalFK == id);
            if (result.Any())
            {
                Images old = result.First();
                System.IO.File.Delete(hostingEnvironment.WebRootPath + "/images/Galeria_" + id + "/" + old.FileName);
                Directory.Delete(hostingEnvironment.WebRootPath + "/images/Galeria_" + id,false);
                _context.Images.Remove(old);
                _context.SaveChanges();
                //System.IO.File.Delete(hostingEnvironment.WebRootPath + "/images/Galeria_" + id);
            }
            var animal = await _context.Animal.FindAsync(id);
            removeAnimalProducts(id);
            removeGodFathers(id);
            _context.Animal.Remove(animal);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Animal eliminado com sucesso!";
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

        private AnimalIndexViewModel getAnimalToList(string AnimalRace, string AnimalType, bool Neutered, bool Disinfection, string SearchString)
        {
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

            if (!string.IsNullOrEmpty(AnimalRace))
            {
                query = query.Where(e=> e.AnimalRaceName == AnimalRace);
            }

            if (!string.IsNullOrEmpty(AnimalType))
            {
                query = query.Where(e => e.AnimaltypeName == AnimalType);
            }

            if (Neutered)
            {
                query = query.Where(e => e.Neutered == true);
            }

            if (Disinfection)
            {
                query = query.Where(e => e.Disinfection == true);
            }

            if (!string.IsNullOrEmpty(SearchString))
            {
                query = query.Where(e => e.Name.Contains(SearchString));
            }

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

            var animalTypeQuery = from animal in _context.AnimalTypes
                                  orderby animal.Name
                                  select animal.Name;

            var animalRaceQuery = from race in _context.AnimalRace
                                   orderby race.Name
                                   select race.Name;

            var animalIndexVM = new AnimalIndexViewModel
            {
                Animals = result,
                Pictures = _context.Images.AsParallel().ToList(),
                UsersNames = _context.Users.AsParallel().ToList(),
                AnimalRacesSelect = new SelectList(animalRaceQuery.Distinct().ToList()),
                AnimalTypesSelect = new SelectList(animalTypeQuery.Distinct().ToList())
            };

            return animalIndexVM;
        }

        private AnimalIndexViewModel getAnimalDetails(int? id)
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

            var animalProductQuery = from animalProduct in _context.AnimalProduct
                                     join prod in _context.Products on animalProduct.ProductFK equals prod.ProductID
                                     join prodType in _context.ProductTypes on prod.ProductTypeFK equals prodType.ProductTypeID
                                     where animalProduct.AnimalFK.Equals(id)
                                     select new
                                     {
                                         ProductID = prod.ProductID,
                                         Name = prod.Name,
                                         ProductTypeName = prodType.Name
                                     };

            var animalProductResult = animalProductQuery.ToList().Select(e => new Product
            {
                ProductID = e.ProductID,
                Name = e.Name,
                ProductTypeName = e.ProductTypeName
            }).ToList();


            var godfatherQuery = from animalUsers in _context.AnimalUsers
                                 join users in _context.Users on animalUsers.UsersFK equals users.UserID
                                 where animalUsers.AnimalFK.Equals(id)
                                 select new
                                 {
                                     UserID = users.UserID,
                                     Name = users.Name
                                 };
            var godfatherResult = godfatherQuery.ToList().Select(e => new Users
            {
                UserID = e.UserID,
                Name = e.Name
            }).ToList();

            var animalIndexVM = new AnimalIndexViewModel
            {
                Animals = result,
                AnimalRaces = _context.AnimalRace.AsParallel().ToList(),
                Pictures = _context.Images.AsParallel().ToList(),
                AnimalTypes = _context.AnimalTypes.AsParallel().ToList(),
                Products = animalProductResult,
                UsersNames = godfatherResult

            };
            return animalIndexVM;
        }

        private void setViewBags(int id)
        {
            /*CREATE */
            if(id == -1)
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

                ViewBag.GodFathers = _context.Users.AsParallel();
                ViewBag.Products = result;
                ViewBag.AnimalTypeFK = _context.AnimalTypes.AsParallel();
                ViewBag.AnimalRaceFK = _context.AnimalRace.AsParallel();
                ViewBag.UsersFK = _context.Users.AsParallel();
            } else
            {
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

                ViewBag.AnimalProducts = _context.AnimalProduct.Where(e => e.AnimalFK == id).ToList();
                ViewBag.GodFathers = _context.AnimalUsers.Where(e => e.AnimalFK == id).ToList();
                ViewBag.Products = productsResult;
                ViewBag.AnimalTypeFK = _context.AnimalTypes.AsParallel();
                ViewBag.AnimalRaceFK = _context.AnimalRace.AsParallel();
                ViewBag.UsersFK = _context.Users.AsParallel();
            }
            /******************************/
        }

        private bool checkValues(Animal animal)
        {
            if (string.IsNullOrEmpty(animal.Name))
            {
                TempData["Message"] = "Por favor insira um nome para o animal!";
                return false;
            }
            if (animal.DateOfBirth.Equals(DateTime.MinValue))
            {
                TempData["Message"] = "Por favor insira uma data de nascimento para o animal!";
                return false;
            }
            if (animal.AnimalTypeFK <= 0)
            {
                TempData["Message"] = "Por favor escolha um tipo de animal!";
                return false;
            }
            if (animal.AnimalRaceFK <= 0)
            {
                TempData["Message"] = "Por favor escolha uma raça de animal!";
                return false;
            }
            if (animal.AnimalID <= -1)
            {
                TempData["Message"] = "Algo de errado aconteceu!";
                return false;
            }
            return true;
        }
    }
}
