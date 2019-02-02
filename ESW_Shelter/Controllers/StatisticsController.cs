using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESW_Shelter.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ESW_Shelter.Controllers
{
    public class StatisticsController : Controller
    {

        private readonly ShelterContext _context;

        public StatisticsController(ShelterContext context) 
        {
            _context = context;
        }

        // GET: Statistics
        public ActionResult Index(int graf, int statistic)
        {
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

        public IActionResult Bar()
        {
            Random rnd = new Random();
            //list of department  
            var lstModel = new List<SimpleReportViewModel>();
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Technology",
                Quantity = rnd.Next(10)
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Sales",
                Quantity = rnd.Next(10)
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Marketing",
                Quantity = rnd.Next(10)
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Human Resource",
                Quantity = rnd.Next(10)
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Research and Development",
                Quantity = rnd.Next(10)
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Acconting",
                Quantity = rnd.Next(10)
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Support",
                Quantity = rnd.Next(10)
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Logistics",
                Quantity = rnd.Next(10)
            });
            return View(lstModel);
        }

        public IActionResult Pie()
        {
            Random rnd = new Random();
            //list of drinks  
            var lstModel = new List<SimpleReportViewModel>();
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Beer",
                Quantity = rnd.Next(10)
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Wine",
                Quantity = rnd.Next(10)
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Whisky",
                Quantity = rnd.Next(10)
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Water",
                Quantity = rnd.Next(10)
            });
            return View(lstModel);
        }

        public IActionResult Line()
        {
            Random rnd = new Random();
            //list of countries  
            var lstModel = new List<SimpleReportViewModel>();
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Brazil",
                Quantity = rnd.Next(10)
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "USA",
                Quantity = rnd.Next(10)
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Portugal",
                Quantity = rnd.Next(10)
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Russia",
                Quantity = rnd.Next(10)
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Ireland",
                Quantity = rnd.Next(10)
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Germany",
                Quantity = rnd.Next(10)
            });
            return View(lstModel);
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