using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ESW_Shelter.Models;
using Microsoft.AspNetCore.Http;
using System.Collections;
using Microsoft.AspNetCore.Authorization;

namespace ESW_Shelter.Controllers
{
    public class DonationsController : SharedController
    {
        private readonly ShelterContext _context;

        public DonationsController(ShelterContext context) : base(context)
        {
            _context = context;
        }

        // GET: Donations
        public async Task<IActionResult> Index(string searchString, string animalType, string productType, string clientString, DateTime dateString)
        {
            if (!GetAutorization(4))
            {
                return ErrorNotFoundOrSomeOtherError();
            }
            var donationUserQuery = from donation in _context.Donation
                        join user in _context.Users on donation.UsersFK equals user.UserID
                        select new
                        {
                            DonationID = donation.DonationID,
                            DateOfDonation = donation.DateOfDonation,
                            UsersFK = donation.UsersFK,
                            UsersName = user.Name
                        };

            if (!String.IsNullOrEmpty(clientString))
            {
                donationUserQuery = donationUserQuery.Where(don => don.UsersName.Contains(clientString));
            }
            if (dateString!=DateTime.MinValue)
            {
                donationUserQuery = donationUserQuery.Where(don => don.DateOfDonation.Day.Equals(dateString.Day) && 
                                        don.DateOfDonation.Month.Equals(dateString.Month) && don.DateOfDonation.Year.Equals(dateString.Year));
            }
            /*if (!String.IsNullOrEmpty(searchString))
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
            }*/
            var result = donationUserQuery.ToList().Select(e => new Donation
            {
                DonationID = e.DonationID,
                DateOfDonation = e.DateOfDonation,
                UsersFK = e.UsersFK,
                UsersName = e.UsersName
            }).ToList();

            foreach(Donation don in result)
            {
                var productsQuery = from donationProduct in _context.DonationProduct
                               join product in _context.Products on donationProduct.ProductFK equals product.ProductID
                               select new
                               {
                                   DonationID = donationProduct.DonationFK,
                                   ProductID = donationProduct.ProductFK,
                                   ProductName = product.Name,
                                   Quantity = donationProduct.Quantity
                               };
                var getAll = productsQuery.ToList().Where(p => p.DonationID == don.DonationID).Select(e => ""+e.ProductName+" - "+e.Quantity);
                String phrase = "";
                foreach (String s in getAll)
                {
                    phrase += s;
                    if(s != getAll.Last())
                    {
                        phrase += " , ";
                    }
                }
                don.ProductName = phrase;
            }

            var animalTypeQuery = from animal in _context.AnimalTypes
                                  orderby animal.Name
                                  select animal.Name;

            var productTypeQuery = from product in _context.ProductTypes
                                   orderby product.Name
                                   select product.Name;
            var usersNameQuery = from user in _context.Users
                                 orderby user.Name
                                 select user.Name;
            DonationIndexViewModel donationIVM = new DonationIndexViewModel
            {
                Donation = result,
                AnimalTypes = new SelectList(animalTypeQuery.Distinct().ToList()),
                ProductTypes = new SelectList(productTypeQuery.Distinct().ToList()),
                UsersNames = new SelectList(usersNameQuery.Distinct().ToList())
            };
            return View(donationIVM);
        }

        // GET: Donations/Details/5
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

            var donation = await _context.Donation
                .FirstOrDefaultAsync(m => m.DonationID == id);
            if (donation == null)
            {
                return NotFound();
            }

            return View(donation);
        }

        // GET: Donations/Create
        public IActionResult Create(string searchString, string animalType, string productType)
        {
            if (!GetAutorization(4))
            {
                return ErrorNotFoundOrSomeOtherError();
            }
            List<Product> selectedProducts = new List<Product>();
            if (ViewBag.selectedProducts != null)
            {
                List<Product> savedProducts = ViewBag.selectedProducts;
                if (savedProducts.Count > 0)
                {
                    selectedProducts = savedProducts;
                    System.Diagnostics.Debug.WriteLine("*************************");
                    System.Diagnostics.Debug.WriteLine(selectedProducts[0].Name);
                    System.Diagnostics.Debug.WriteLine("*************************");
                }
            }
            if (Request.HasFormContentType && Request.Form != null && Request.Form.Count() > 0)
            {
                string selected = Request.Form["checkProduct"].ToString();
                string[] selectedList = selected.Split(',');

                foreach (var temp in selectedList)
                {
                    int prodKey = Convert.ToInt32(temp);
                    int newQuant = Convert.ToInt32(Request.Form["quantityProduct " + Convert.ToInt32(temp)].ToString());

                    selectedProducts.Add(new Product
                    {
                        ProductID = prodKey,
                        ProductTypeFK = prodKey,
                        Quantity = newQuant
                    });
                }
            }

            /**
             * Produtos
             */
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
                ProductTypeName = e.ProductTypeName,
                AnimaltypeName = e.AnimaltypeName
            }).ToList();


            var animalTypeQuery = from animal in _context.AnimalTypes
                                  orderby animal.Name
                                  select animal.Name;

            var productTypeQuery = from product in _context.ProductTypes
                                   orderby product.Name
                                   select product.Name;

            var donationIndexVM = new DonationIndexViewModel
            {
                Products = result,
                AnimalTypes = new SelectList(animalTypeQuery.Distinct().ToList()),
                ProductTypes = new SelectList(productTypeQuery.Distinct().ToList()),
                SelectedProducts = selectedProducts
            };
            /**
             * 
             */
            ViewBag.UsersFK = _context.Users.AsParallel();
            ViewBag.selectedProducts = selectedProducts;
            return View(donationIndexVM);
        }

        // POST: Donations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DonationID,DateOfDonation,UsersFK")] DonationIndexViewModel donationIVM)
        {
            if (!GetAutorization(4))
            {
                return ErrorNotFoundOrSomeOtherError();
            }

            if (!checkValues(donationIVM))
            {
                return Redirect(nameof(Create));
            }

            if (ModelState.IsValid)
            {
                   
                string selected = Request.Form["checkProduct"].ToString();
                string[] selectedList = selected.Split(',');

                Donation donation = new Donation
                {
                    DateOfDonation = donationIVM.DateOfDonation,
                    UsersFK = donationIVM.UsersFK
                };
                _context.Add(donation);
                _context.SaveChanges();

                if(selectedList[0] != "")
                {
                    foreach (var temp in selectedList)
                    {

                        int prodKey = Convert.ToInt32(temp);
                        int newQuant = Convert.ToInt32(Request.Form["quantityProduct " + Convert.ToInt32(temp)].ToString());
                        DonationProduct donationProduct = new DonationProduct
                        {
                            DonationFK = donation.DonationID,
                            ProductFK = prodKey,
                            Quantity = newQuant
                        };
                        var prod = new Product
                        {
                            ProductID = prodKey
                        };
                        prod.Quantity = _context.Products.Where(e => e.ProductID == prodKey).FirstOrDefault().Quantity + newQuant;
                        _context.Entry(prod).Property("Quantity").IsModified = true;
                        _context.SaveChanges();
                        _context.Add(donationProduct);
                    }
                }

                await _context.SaveChangesAsync();
                TempData["Message"] = "Doacao criada com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            return View(donationIVM);
        }

        // GET: Donations/Edit/5
        public async Task<IActionResult> Edit(int? id, string searchString, string animalType, string productType)
        {
            if (!GetAutorization(4))
            {
                return ErrorNotFoundOrSomeOtherError();
            }
            if (id == null)
            {
                return NotFound();
            }

            var donation = await _context.Donation.FindAsync(id);
            if (donation == null)
            {
                return NotFound();
            }

            /**
             * Produtos
             */
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
                ProductTypeName = e.ProductTypeName,
                AnimaltypeName = e.AnimaltypeName
            }).ToList();


            var animalTypeQuery = from animal in _context.AnimalTypes
                                  orderby animal.Name
                                  select animal.Name;

            var productTypeQuery = from product in _context.ProductTypes
                                   orderby product.Name
                                   select product.Name;

            /**
             * 
             */
            var queryDonationProducts = _context.DonationProduct.Where(e => e.DonationFK == id);

            var donationProducts = queryDonationProducts.ToList();
            var donationIndexVM = new DonationIndexViewModel
            {
                Products = result,
                EditDonation = donation,
                AnimalTypes = new SelectList(animalTypeQuery.Distinct().ToList()),
                ProductTypes = new SelectList(productTypeQuery.Distinct().ToList()),
                DonationProducts = donationProducts
            };
            string date31string = donationIndexVM.EditDonation.DateOfDonation.ToString("yyyy/MM/dd");
            donationIndexVM.EditDonation.DateOfDonation = DateTime.ParseExact(date31string, "yyyy/MM/dd", null);
            ViewBag.UsersFK = _context.Users.AsParallel();
            return View(donationIndexVM);
        }

        // POST: Donations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DonationID,DateOfDonation,UsersFK")] DonationIndexViewModel donationIVM)
        {
            /*if (id != donationIVM.EditDonation.DonationID)
            {
                return NotFound();
            }*/
            if (!GetAutorization(4))
            {
                return ErrorNotFoundOrSomeOtherError();
            }

            if (!checkValues(donationIVM))
            {
                return Redirect(nameof(Create));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string selected = Request.Form["checkProduct"].ToString();
                    string[] selectedList = selected.Split(',');
                    /*System.Diagnostics.Debug.WriteLine("*************************");
                    System.Diagnostics.Debug.WriteLine(selectedList[0]=="");
                    System.Diagnostics.Debug.WriteLine("*************************");*/
                    var result2 = _context.Donation.Find(id);
                    if (result2 != null)
                    {
                        result2.DateOfDonation = donationIVM.DateOfDonation;
                        _context.SaveChanges();
                    }
                    /*Donation donation = new Donation
                    {
                        WDateOfDonation = donationIVM.DateOfDonation,
                        UsersFK = donationIVM.UsersFK
                    };
                    _context.Update(donation);
                    _context.SaveChanges();*/
                    var x = 0;
                    var allToRemove = _context.DonationProduct.Where(p => p.DonationFK == id);
                    foreach (DonationProduct don in allToRemove)
                    {
                        var result3 = _context.Products.SingleOrDefault(p => p.ProductID == don.ProductFK);

                        if (result3 != null)
                        {
                            result3.Quantity -= don.Quantity;
                           // _context.SaveChangesAsync();
                            _context.DonationProduct.Remove(don);

                        }
                    }
                    if(selectedList[0]!="")
                    {
                        foreach (var temp in selectedList)
                        {
                            int prodKey = Convert.ToInt32(temp);
                            int newQuant = Convert.ToInt32(Request.Form["quantityProduct " + Convert.ToInt32(temp)].ToString());
                            DonationProduct donationProduct = new DonationProduct
                            {
                                DonationFK = id,
                                ProductFK = prodKey,
                                Quantity = newQuant
                            };
                            x++;
                            var quantityToRemove = allToRemove.Where(p => p.ProductFK == prodKey && p.DonationFK == id).ToList();
                            var result = _context.Products.SingleOrDefault(p => p.ProductID == prodKey);
                            if (result != null)
                            {
                                result.Quantity += newQuant;
                                _context.SaveChanges();
                            }

                            _context.SaveChanges();
                            _context.Add(donationProduct);
                        }
                    }
                    var editdon = _context.Donation.SingleOrDefault(d => d.DonationID == id);
                    if (editdon != null)
                    {
                        editdon.DateOfDonation = donationIVM.DateOfDonation;
                        editdon.UsersFK = donationIVM.UsersFK;
                        _context.SaveChanges();
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonationExists(donationIVM.EditDonation.DonationID))
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
            return View(donationIVM);
        }

        private async void removeForEdition(int donationID)
        {
            var alldonations = _context.DonationProduct.Where(e=> e.DonationFK == donationID);
            foreach (DonationProduct donProd in alldonations)
            {
                var prod = new Product
                {
                    ProductID = donProd.ProductFK
                };
                Product prodToTakeOff = _context.Products.Where(e => e.ProductID == donProd.ProductFK).First();
                prod.Quantity = prodToTakeOff.Quantity - donProd.Quantity;
                _context.Entry(prod).Property("Quantity").IsModified = true;
                _context.DonationProduct.Remove(donProd);
                await _context.SaveChangesAsync();
            }
        }
        // GET: Donations/Delete/5
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

            var donation = await _context.Donation
                .FirstOrDefaultAsync(m => m.DonationID == id);
            if (donation == null)
            {
                return NotFound();
            }

            return View(donation);
        }

        // POST: Donations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!GetAutorization(4))
            {
                return ErrorNotFoundOrSomeOtherError();
            }
            var donation = await _context.Donation.FindAsync(id);
            var query = from donationProduct in _context.DonationProduct
                        where donationProduct.DonationFK == id
                        select donationProduct.DonationProductID;

            foreach(int queryid in query)
            {
                var donationProduct = _context.DonationProduct.Find(queryid);
                var result3 = _context.Products.SingleOrDefault(p => p.ProductID == donationProduct.ProductFK);

                if (result3 != null)
                {
                    result3.Quantity -= donationProduct.Quantity;
                }
                _context.DonationProduct.Remove(donationProduct);
            }
            _context.Donation.Remove(donation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        /*
                var result3 = _context.Products.SingleOrDefault(p => p.ProductID == don.ProductFK);

                if (result3 != null)
                {
                    result3.Quantity -= don.Quantity;
                    // _context.SaveChangesAsync();
                    _context.DonationProduct.Remove(don);

                }
             */
        private bool DonationExists(int id)
        {
            return _context.Donation.Any(e => e.DonationID == id);
        }

        private bool checkValues(DonationIndexViewModel donationIVM)
        {
            if (donationIVM.DateOfDonation == DateTime.MinValue)
            {
                TempData["Message"] = "Escolha uma data para a doação!";
                return false;
            }
            if (donationIVM.UsersFK <= -1)
            {
                TempData["Message"] = "Escolha uma cliente para a doação!";
                return false;
            }
            return true;
        }
    }
}

        public async Task<IActionResult> Index()
        {
            StripeLib stripeLib = new StripeLib();
            var plans = stripeLib.GetPlans();

            ViewData["plans"] = plans;
            
            return View();
        }
        // GET: Donations
        // GET: Donations/Create
        }
            return View();

            ViewData["plans"] = plans;

            var plans = stripeLib.GetPlans();
            StripeLib stripeLib = new StripeLib();
        {
        public IActionResult Create()
        // POST: Donations/Subscribe
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public string Subscribe(string planId)
        {
            try
            {
                var userId = Int32.Parse(HttpContext.Session.GetString("UserID"));
                var user = _context.Users.Find(userId);
                var customerId = user.CustomerId;

                StripeLib stripeLib = new StripeLib();

                var subscriptionId = stripeLib.Subscribe(customerId, planId);
                if (subscriptionId == null) return "false";
                return subscriptionId;

            } catch (Exception ex)
            {
                return "false";
            }
        }