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
using Microsoft.SqlServer;
using Microsoft.Extensions.DependencyInjection;

namespace ESW_Shelter_Tests
{
    public class ProductTypeControllerTests
    {
        private ProductsController repository;
        public static DbContextOptions<ShelterContext> options { get; }
        public static string connectionString = "Server=(localdb)\\mssqllocaldb;Database=ShelterDB;Trusted_Connection=True;MultipleActiveResultSets=true";

        static ProductTypeControllerTests()
        {
            options = new DbContextOptionsBuilder<ShelterContext>()
                .UseSqlServer(connectionString).Options;

        }
        [Fact]
        public async Task Index_CanLoadFromContext()
        {


            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                context.ProductTypes.AddRange(
                    new ProductType { Name = "ProdutoTipo1" },
                    new ProductType { Name = "ProdutoTipo2"},
                    new ProductType { Name = "ProdutoTipo3" });
                context.SaveChanges();
            }
            using (var context = new ShelterContext(options))
            {
                var controller = new ProductTypesController(context);
                var result = await controller.Index();

                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<IEnumerable<ProductType>>(
                    viewResult.ViewData.Model);
                Assert.Equal(3, model.Count());
            }
        }

        [Fact]
        public async Task EditProductType_ReturnsNotFoundResult_WhenIdIsDiferentFromModelId()
        {


            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
            }
            using (var context = new ShelterContext(options))
            {
                var controller = new ProductTypesController(context);
                var result = await controller.Edit(1, new ProductType { ProductTypeID = 2 });

                Assert.IsType<NotFoundResult>(result);
            }
        }

        [Fact]
        public async Task EditProductType_ReturnsViewResult_WhenModelStateIsInvalid()
        {


            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
            }
            using (var context = new ShelterContext(options))
            {
                var controller = new ProductTypesController(context);
                controller.ModelState.AddModelError("Erro", "Texto erro");

                var result = await controller.Edit(2, new ProductType { ProductTypeID = 2 });

                Assert.IsType<ViewResult>(result);
            }
        }

    }
}

 
