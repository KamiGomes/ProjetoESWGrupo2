using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System;

using ESW_Shelter.Controllers;
using ESW_Shelter.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Http;

namespace AnimalTypeControllerTest
{
    public class AnimalTypeControllerTest
    {

        private AnimalTypesController repository;
        public static DbContextOptions<ShelterContext> options { get; }
        public static string connectionString = "Server=(localdb)\\mssqllocaldb;Database=ShelterDB;Trusted_Connection=True;MultipleActiveResultSets=true";

        static AnimalTypeControllerTest()
        {
            options = new DbContextOptionsBuilder<ShelterContext>()
                .UseSqlServer(connectionString).Options;

        }
        [Fact]
        public async Task Index_CanFIndAnimalType_FromContext_NullParameters()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                context.AnimalTypes.AddRange(
                    new AnimalType { Name = "animal1" },
                    new AnimalType { Name = "animal2" },
                    new AnimalType { Name = "animal3" });

                context.SaveChanges();
                var controller = new AnimalTypesController(context);
                var result = await controller.Index();

                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<IEnumerable<AnimalType>>(viewResult.ViewData.Model);
                Assert.IsType<ViewResult>(result);

                //Assert.IsType<List<AnimalType>>(result);
                //var viewResult = Assert.IsType<ViewResult>(result);
                //var model = Assert.IsAssignableFrom<AnimalType>(
                //    viewResult.ViewData.Model);
                //Assert.IsType<List<AnimalType>>(model);
            }
        }

        [Fact]
        public async Task Create_CanCallForm()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                var controller = new AnimalTypesController(context);
                var result = await controller.Create();//Isto mais tarde é para se alterar
                                                       /*var viewResult = Assert.IsType<ViewResult>(result);
                                                       var model = Assert.IsAssignableFrom<DonationIndexViewModel>(viewResult.ViewData.Model);*/
                Assert.IsType<ViewResult>(result);

            }
        }

        [Fact]
        public async Task Create_ReturnsBadRequest_GivenInvalidModel()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                var controller = new AnimalTypesController(context);
                controller.ModelState.AddModelError("error", "some error");
                var result = await controller.Create(animalType: null);

                Assert.IsType<ViewResult>(result);

            }

        }

        [Fact]
        public async Task CreateAnimalTypes_CreateSucess()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                var controller = new AnimalTypesController(context);
                controller.ModelState.AddModelError("error", "some error");

                var result = await controller.Create(new AnimalType { Name = "sas" });

                Assert.IsType<ViewResult>(result);
            }
        }

        [Fact]
        public async Task EditAnimalTypes_GetForm_WhenIdIsDiferentFromModelId()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                var controller = new AnimalTypesController(context);
                var result = await controller.Edit(99999999);

                Assert.IsType<NotFoundResult>(result);
            }
        }

        [Fact]
        public async Task EditAnimalTypes_PostForm_GivenInvalidModel()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                var controller = new AnimalTypesController(context);
                var result = await controller.Edit(2, new AnimalType() { AnimalTypeID = 2, Name = "" });

                Assert.IsType<ViewResult>(result);
            }
        }

        [Fact]
        public async Task EditAnimalTypes_PostForm_ValuesNotValid()
        {
            using (var context = new ShelterContext(options))
            {

                context.Database.EnsureCreated();
                var controller = new AnimalTypesController(context);
                var result = await controller.Edit(1, new AnimalType { Name = "" });

                Assert.IsType<ViewResult>(result);
            }
        }

        [Fact]
        public async Task EditAnimalTypes_PostForm_DifferentIdFromGivenId()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                var controller = new AnimalTypesController(context);
                var result = await controller.Edit(2, new AnimalType { AnimalTypeID = 3, Name = "sasa" });

                Assert.IsType<RedirectToActionResult>(result);
            }
        }

        [Fact]
        public async Task EditAnimalTypes_PostForm_Sussess()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                var controller = new AnimalTypesController(context);
                var result = await controller.Edit(2, new AnimalType() { AnimalTypeID = 2, Name = "Nome1" });

                Assert.IsType<RedirectToActionResult>(result);
            }
        }

        [Fact]
        public async Task DeleteAnimalTypes_GetForm_InvalidID()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                context.AnimalTypes.AddRange(
                   new AnimalType { Name = "animal1" },
                    new AnimalType { Name = "animal2" },
                    new AnimalType { Name = "animal3" });
                context.SaveChanges();
                var controller = new AnimalTypesController(context);
                var result = await controller.Delete(0);
                Assert.IsType<NotFoundResult>(result);

            }
        }

        [Fact]
        public async Task DeleteAnimalTypes_GetForm_InvalidModel()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                context.AnimalTypes.AddRange(
                   new AnimalType { Name = "animal1" },
                    new AnimalType { Name = "animal2" },
                    new AnimalType { Name = "animal3" });
                context.SaveChanges();
                var controller = new AnimalTypesController(context);
                var result = await controller.Delete(9999999);
                Assert.IsType<NotFoundResult>(result);

            }
        }

        [Fact]
        public async Task DeleteAnimalTypes_GetForm_FoundAnimalType()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                context.AnimalTypes.AddRange(
                   new AnimalType { Name = "animal1" },
                    new AnimalType { Name = "animal2" },
                    new AnimalType { Name = "animal3" });
                context.SaveChanges();
                var controller = new AnimalTypesController(context);
                var result = await controller.Delete(1002);
                Assert.IsType<ViewResult>(result);

            }
        }



        [Fact]
        public async Task DeleteConfirmationAnimalTypes_Sucesseful()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                context.AnimalTypes.AddRange(
                    new AnimalType { Name = "animal1" },
                    new AnimalType { Name = "animal2" },
                    new AnimalType { Name = "animal3" });
                context.SaveChanges();
                var controller = new AnimalTypesController(context);
                var result = await controller.DeleteConfirmed(1002);
                Assert.IsType<RedirectToActionResult>(result);

            }
        }
    }
}
