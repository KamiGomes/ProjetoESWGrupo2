using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System;
using System.Text;
using ESW_Shelter.Controllers;
using ESW_Shelter.Models;

namespace ESW_Shelter_Tests
{
    public class ProductsControllerTests
    {
        private ProductsController repository;
        public static DbContextOptions<ShelterContext> options { get; }
        public static string connectionString = "Server=(localdb)\\mssqllocaldb;Database=ShelterDB;Trusted_Connection=True;MultipleActiveResultSets=true";

        static ProductsControllerTests()
        {
                options = new DbContextOptionsBuilder<ShelterContext>()
                .UseSqlServer(connectionString)
                .Options;
        }

        [Fact]
        public async Task Index_CanLoadFromContext()
        {

            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                context.Products.AddRange(
                    new Product { Name = "Produto1", Quantity = 10 , AnimalTypeFK=1, ProductTypeFK=1, ProductTypeName = "Comida", AnimaltypeName="Cão"},
                    new Product { Name = "Produto2", Quantity = 30, AnimalTypeFK = 1, ProductTypeFK = 1, ProductTypeName = "Comida", AnimaltypeName = "Cão" },
                    new Product { Name = "Produto3", Quantity = 99, AnimalTypeFK = 1, ProductTypeFK = 1, ProductTypeName = "Comida", AnimaltypeName = "Cão" });
                context.SaveChanges();
            }
            using (var context = new ShelterContext(options))
            {
                var controller = new ProductsController(context);
                var result = await controller.Index("","","");

                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<ProductIndexViewModel>(
                    viewResult.ViewData.Model);
                Assert.IsType<ProductIndexViewModel>(model);
            }
        }

        [Fact]
        public async Task EditProduct_ReturnsNotFoundResult_WhenIdIsDiferentFromModelId()
        {

            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
            }
            using (var context = new ShelterContext(options))
            {
                var controller = new ProductsController(context);
                var result = await controller.Edit(1, new Product { ProductID = 2 });

                Assert.IsType<NotFoundResult>(result);
            }
        }

        [Fact]
        public async Task EditProduct_ReturnsViewResult_WhenModelStateIsInvalid()
        {

            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
            }
            using (var context = new ShelterContext(options))
            {
                var controller = new ProductsController(context);
                controller.ModelState.AddModelError("Erro", "Texto erro");

                var result = await controller.Edit(2, new Product { ProductID = 2 });

                Assert.IsType<ViewResult>(result);
            }
        }

    }
}
