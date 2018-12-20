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
    public class AnimalTypeControllerTests
    {
        private ProductsController repository;
        public static DbContextOptions<ShelterContext> options { get; }
        public static string connectionString = "Server=(localdb)\\mssqllocaldb;Database=ShelterDB;Trusted_Connection=True;MultipleActiveResultSets=true";

        static AnimalTypeControllerTests()
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
                context.AnimalTypes.AddRange(
                    new AnimalType { Name = "ProdutoTipo1" },
                    new AnimalType { Name = "ProdutoTipo2" },
                    new AnimalType { Name = "ProdutoTipo3" });
                context.SaveChanges();
            }
            using (var context = new ShelterContext(options))
            {
                var controller = new AnimalTypesController(context);
                var result = await controller.Index();

                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<IEnumerable<AnimalType>>(
                    viewResult.ViewData.Model);
                Assert.Equal(3, model.Count());
            }
        }

        [Fact]
        public async Task EditAnimalType_ReturnsNotFoundResult_WhenIdIsDiferentFromModelId()
        {


            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
            }
            using (var context = new ShelterContext(options))
            {
                var controller = new AnimalTypesController(context);
                var result = await controller.Edit(1, new AnimalType { AnimalTypeID = 2 });

                Assert.IsType<NotFoundResult>(result);
            }
        }

        [Fact]
        public async Task EditAnimalType_ReturnsViewResult_WhenModelStateIsInvalid()
        {


            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
            }
            using (var context = new ShelterContext(options))
            {
                var controller = new AnimalTypesController(context);
                controller.ModelState.AddModelError("Erro", "Texto erro");

                var result = await controller.Edit(2, new AnimalType { AnimalTypeID = 2 });

                Assert.IsType<ViewResult>(result);
            }
        }

    }
}


