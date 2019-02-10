using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESW_Shelter.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ESW_Shelter.Controllers
{
    public class StatisticsController : SharedController
    {

        private readonly ShelterContext _context;

        public StatisticsController(ShelterContext context) : base(context)
        {
            _context = context;
        }

        // GET: Statistics
        public ActionResult Index(int graf, int statistic)
        {
            if (!GetAuthorization(9, 'r'))
            {
                return NotFound();
            }
            ViewBag.Permission = getPermissions();
            var lstModel = new List<SimpleReportViewModel>();
            switch (statistic)
            {
                case 0:
                    (int owned, int noOwner) = getAnimalsNoOwner();
                    lstModel.Add(new SimpleReportViewModel
                    {
                        DimensionOne = "Animais por adoptar",
                        Quantity = noOwner
                    });
                    lstModel.Add(new SimpleReportViewModel
                    {
                        DimensionOne = "Animais adoptados",
                        Quantity = owned
                    });
                    ViewBag.labelToUse = "Animais Adoptados e por Adoptar";
                    break;
                case 1:
                    (int animalGodfathers, int allAnimals) = getAnimalNoGodFathers();
                    lstModel.Add(new SimpleReportViewModel
                    {
                        DimensionOne = "Animais com Padrinhos",
                        Quantity = animalGodfathers
                    });
                    lstModel.Add(new SimpleReportViewModel
                    {
                        DimensionOne = "Animais sem Padrinhos",
                        Quantity = allAnimals
                    });
                    ViewBag.labelToUse = "Animais Com e Sem padrinhos";
                    break;
                case 2:
                    (int animalProducts, int allAnimalsProducts) = getAnimalNoGodFathers();
                    lstModel.Add(new SimpleReportViewModel
                    {
                        DimensionOne = "Animais com necessidades especiais",
                        Quantity = animalProducts
                    });
                    lstModel.Add(new SimpleReportViewModel
                    {
                        DimensionOne = "Animais sem necessidades especiais",
                        Quantity = allAnimalsProducts
                    });
                    ViewBag.labelToUse = "Animais Com e Sem Necessidades Especiais";
                    break;
                case 3:
                    List<LoginStatistic> allLogins = _context.LoginStatistic.AsParallel().Where(e=> e.DateStatistic.Month == DateTime.Today.Month 
                                                                                                    && e.DateStatistic.Year == DateTime.Today.Year).OrderBy(e => e.DateStatistic).ToList();
                    foreach(LoginStatistic log in allLogins)
                    {
                        lstModel.Add(new SimpleReportViewModel {
                            DimensionOne = log.DateStatistic.ToString(),
                            Quantity = log.Count
                        });
                    }
                    ViewBag.labelToUse = "Logins efetuados por dia [Mês corrente]";
                    break;
                case 5:
                    List<DateTime> montlyDonation = _context.Donation.Where(e=> e.DateOfDonation.Month == DateTime.Today.Month && e.DateOfDonation.Year == DateTime.Today.Year).OrderBy(e=> e.DateOfDonation).Select(e => e.DateOfDonation).Distinct().ToList();
                    List<SelectListItem> toStatisticsYouGo = new List<SelectListItem> ();
                    foreach (DateTime don in montlyDonation)
                    {
                        int total = _context.Donation.Where(e => e.DateOfDonation == don).Count();
                        lstModel.Add(new SimpleReportViewModel
                        {
                            DimensionOne = don.ToString(),
                            Quantity = total
                        });
                    }
                    ViewBag.labelToUse = "Doações efetuados por dia [Mês corrente]";
                    break;
                case 7:
                    List<RegisterStatistics> registerStatistics = _context.RegisterStatistics.AsParallel().Where(e => e.DateStatistic.Month == DateTime.Today.Month
                                                                                                    && e.DateStatistic.Year == DateTime.Today.Year).OrderBy(e => e.DateStatistic).ToList();
                    foreach (RegisterStatistics regis in registerStatistics)
                    {
                        lstModel.Add(new SimpleReportViewModel
                        {
                            DimensionOne = regis.DateStatistic.ToString(),
                            Quantity = regis.Count
                        });
                    }
                    ViewBag.labelToUse = "Registos efetuados por dia [Mês corrente]";
                    break;
                case 8:
                    List<Product> productsStockWeek = _context.Products.AsParallel().ToList();
                    foreach (Product prod in productsStockWeek)
                    {
                        lstModel.Add(new SimpleReportViewModel
                        {
                            DimensionOne = prod.Name+"- Stock Semanal: "+ prod.WeekStock+", Stock Mensal: "+prod.MonthStock,
                            Quantity = prod.Quantity
                        });
                    }
                    ViewBag.labelToUse = "Quantidade do Stock de Produtos Existentes [Para semana e Mês] ";
                    break;
                default:
                    graf = 0;
                    break;
            }
            switch (graf)
            {
                case 0:
                    ViewBag.typeOfChart = "bar";
                    break;
                case 1:
                    ViewBag.typeOfChart = "pie";
                    break;
                case 2:
                    ViewBag.typeOfChart = "line";
                    break;
                default:
                    break;
            }
            return View(lstModel);
        }

        public ActionResult IndexStacked(int statistic)
        {
            if (!GetAuthorization(9, 'r'))
            {
                return NotFound();
            }
            ViewBag.Permission = getPermissions();
            var lstModel = new List<StackedViewModel>();
            switch (statistic)
            {
                case 0:
                    var total = from donP in _context.DonationProduct
                                join don in _context.Donation on donP.DonationFK equals don.DonationID
                                group donP by new { donP.ProductFK, don.DateOfDonation } into gGroup
                                select new
                                {
                                    Total = gGroup.Sum(e=> e.Quantity),
                                    Product = gGroup.Key.ProductFK,
                                    Date = gGroup.Key.DateOfDonation
                                };

                    var listed = total.ToList();

                    var allDonationsOfMonth = _context.Donation.Where(e => e.DateOfDonation.Month == DateTime.Today.Month && e.DateOfDonation.Year == DateTime.Today.Year).Distinct();
                    if(allDonationsOfMonth.Any())
                    {
                        foreach (Donation don in allDonationsOfMonth.ToList())
                        {

                            var ofDate = total.Where(e => e.Date == don.DateOfDonation);

                            if(ofDate.Any())
                            {
                                List<SimpleReportViewModel> allProductsInDate = ofDate.ToList().Select(e => new SimpleReportViewModel()
                                {
                                    DimensionOne = _context.Products.Where(p => p.ProductID == e.Product).First().Name,
                                    Quantity = e.Total
                                }).ToList();

                                lstModel.Add(new StackedViewModel()
                                {
                                    StackedDimensionOne = don.DateOfDonation.ToString(),
                                    LstData = allProductsInDate
                                });
                            }
                        }
                    }

                    break;
                default:
                    break;
            }
            return View(lstModel);
        }

        private (int,int) getAnimalsNoOwner()
        {
            int owned = _context.Animal.Where(e => e.OwnerFK != 0).Count();
            int noOwner = _context.Animal.Where(e => e.OwnerFK == 0).Count();
            return (owned, noOwner);
        }

        private (int,int) getAnimalNoGodFathers()
        {
            int animalGodfathers = _context.AnimalUsers.Select(e => e.AnimalFK).Distinct().Count();
            int allAnimals = _context.Animal.AsParallel().Count();
            return (animalGodfathers, allAnimals-animalGodfathers < 0 ? 0 : allAnimals-animalGodfathers);
        }

        private (int,int) getAnimalWithProducts()
        {
            int animalProducts = _context.AnimalProduct.Select(e => e.AnimalFK).Distinct().Count();
            int allAnimals = _context.Animal.AsParallel().Count();
            return (animalProducts, allAnimals - animalProducts < 0 ? 0 : allAnimals - animalProducts);
        }

       

        public IActionResult Stacked()
        {
            Random rnd = new Random();
            var lstModel = new List<StackedViewModel>();
            //sales of product sales by quarter  
            lstModel.Add(new StackedViewModel
            {
                StackedDimensionOne = "First Quarter",
                LstData = new List<SimpleReportViewModel>()
                   {
                       new SimpleReportViewModel()
        {
            DimensionOne = "TV",
                           Quantity = rnd.Next(10)
                       },
                       new SimpleReportViewModel()
        {
            DimensionOne = "Games",
                           Quantity = rnd.Next(10)
                       },
                       new SimpleReportViewModel()
        {
            DimensionOne = "Books",
                           Quantity = rnd.Next(10)
                       }
    }
            });
            lstModel.Add(new StackedViewModel
            {
                StackedDimensionOne = "Second Quarter",
                LstData = new List<SimpleReportViewModel>()
                   {
                       new SimpleReportViewModel()
{
    DimensionOne = "TV",
                           Quantity = rnd.Next(10)
                       },
                       new SimpleReportViewModel()
{
    DimensionOne = "Games",
                           Quantity = rnd.Next(10)
                       },
                       new SimpleReportViewModel()
{
    DimensionOne = "Books",
                           Quantity = rnd.Next(10)
                       }
                   }
            });
            lstModel.Add(new StackedViewModel
            {
                StackedDimensionOne = "Third Quarter",
                LstData = new List<SimpleReportViewModel>()
                   {
                       new SimpleReportViewModel()
{
    DimensionOne = "TV",
                           Quantity = rnd.Next(10)
                       },
                       new SimpleReportViewModel()
{
    DimensionOne = "Games",
                           Quantity = rnd.Next(10)
                       },
                       new SimpleReportViewModel()
{
    DimensionOne = "Books",
                           Quantity = rnd.Next(10)
                       }
                   }
            });
            lstModel.Add(new StackedViewModel
            {
                StackedDimensionOne = "Fourth Quarter",
                LstData = new List<SimpleReportViewModel>()
                   {
                       new SimpleReportViewModel()
{
    DimensionOne = "TV",
                           Quantity = rnd.Next(10)
                       },
                       new SimpleReportViewModel()
{
    DimensionOne = "Games",
                           Quantity = rnd.Next(10)
                       },
                       new SimpleReportViewModel()
{
    DimensionOne = "Books",
                           Quantity = rnd.Next(10)
                       }
                   }
            });
            return View(lstModel);
        }

    }
}