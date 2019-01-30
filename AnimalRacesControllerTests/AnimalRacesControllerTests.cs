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
namespace AnimalRacesControllerTests
{
    public class AnimalRacesControllerTests
    {
        private AnimalRacesController repository;
        public static DbContextOptions<ShelterContext> options { get; }
        public static string connectionString = "Server=(localdb)\\mssqllocaldb;Database=ShelterDB;Trusted_Connection=True;MultipleActiveResultSets=true";

        static AnimalRacesControllerTests()
        {
            options = new DbContextOptionsBuilder<ShelterContext>()
                .UseSqlServer(connectionString).Options;

        }

        [Fact]
        public async Task Index_CanFIndAnimalRaces_FromContext_NullParameters()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                context.AnimalRace.AddRange(
                    new AnimalRace { Name = "animal1" },
                    new AnimalRace { Name = "animal2" },
                    new AnimalRace { Name = "animal3" });

                context.SaveChanges();
                var controller = new AnimalRacesController(context);
                var result = await controller.Index();

                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<IEnumerable<AnimalRace>>(viewResult.ViewData.Model);
                Assert.IsType<ViewResult>(result);

                //Assert.IsType<List<AnimalType>>(result);
                //var viewResult = Assert.IsType<ViewResult>(result);
                //var model = Assert.IsAssignableFrom<AnimalType>(
                //    viewResult.ViewData.Model);
                //Assert.IsType<List<AnimalType>>(model);
            }
        }

        [Fact]
        public void Create_CanCallForm()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                var controller = new AnimalRacesController(context);
                var result = controller.Create();//Isto mais tarde é para se alterar
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
                var controller = new AnimalRacesController(context);
                controller.ModelState.AddModelError("error", "some error");
                var result = await controller.Create(animalRace: null);

                Assert.IsType<ViewResult>(result);

            }

        }

        [Fact]
        public async Task CreateAnimalRaces_CreateSucess()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                var controller = new AnimalRacesController(context);
                controller.ModelState.AddModelError("error", "some error");

                var result = await controller.Create(new AnimalRace { Name = "sas" });

                Assert.IsType<ViewResult>(result);
            }
        }

        [Fact]
        public async Task EditAnimalRaces_GetForm_WhenIdIsDiferentFromModelId()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                var controller = new AnimalRacesController(context);
                var result = await controller.Edit(99999999);

                Assert.IsType<NotFoundResult>(result);
            }
        }

        [Fact]
        public async Task EditAnimalRaces_PostForm_GivenInvalidModel()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                var controller = new AnimalRacesController(context);
                var result = await controller.Edit(2, new AnimalRace() { AnimalRaceID = 2, Name = "" });

                Assert.IsType<NotFoundResult>(result);
            }
        }

        [Fact]
        public async Task EditAnimalRaces_PostForm_ValuesNotValid()
        {
            using (var context = new ShelterContext(options))
            {

                context.Database.EnsureCreated();
                var controller = new AnimalRacesController(context);
                var result = await controller.Edit(1, new AnimalRace { Name = "" });

                Assert.IsType<NotFoundResult>(result);
            }
        }

        [Fact]
        public async Task EditAnimalRaces_PostForm_DifferentIdFromGivenId()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                var controller = new AnimalRacesController(context);
                var result = await controller.Edit(2, new AnimalRace { AnimalRaceID = 3, Name = "sasa" });

                Assert.IsType<NotFoundResult>(result);
            }
        }

        [Fact]
        public async Task EditAnimalRaces_PostForm_Sussess()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                var controller = new AnimalRacesController(context);
                var result = await controller.Edit(2, new AnimalRace() { AnimalRaceID = 2, Name = "Nome1" });

                Assert.IsType<RedirectToActionResult>(result);
            }
        }

        [Fact]
        public async Task DeleteAnimalRaces_GetForm_InvalidID()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                context.AnimalRace.AddRange(
                   new AnimalRace { Name = "animal1" },
                    new AnimalRace { Name = "animal2" },
                    new AnimalRace { Name = "animal3" });
                context.SaveChanges();
                var controller = new AnimalRacesController(context);
                var result = await controller.Delete(0);
                Assert.IsType<NotFoundResult>(result);

            }
        }

        [Fact]
        public async Task DeleteAnimalRaces_GetForm_InvalidModel()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                context.AnimalRace.AddRange(
                   new AnimalRace { Name = "animal1" },
                    new AnimalRace { Name = "animal2" },
                    new AnimalRace { Name = "animal3" });
                context.SaveChanges();
                var controller = new AnimalRacesController(context);
                var result = await controller.Delete(9999999);
                Assert.IsType<NotFoundResult>(result);

            }
        }

        [Fact]
        public async Task DeleteAnimalRaces_GetForm_FoundAnimalType()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                context.AnimalRace.AddRange(
                   new AnimalRace { Name = "animal1" },
                    new AnimalRace { Name = "animal2" },
                    new AnimalRace { Name = "animal3" });
                context.SaveChanges();
                var controller = new AnimalRacesController(context);
                var result = await controller.Delete(8);
                Assert.IsType<ViewResult>(result);

            }
        }

        [Fact]
        public async Task DeleteConfirmationAnimalRaces_Sucesseful()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                context.AnimalRace.AddRange(
                    new AnimalRace { Name = "animal1" },
                    new AnimalRace { Name = "animal2" },
                    new AnimalRace { Name = "animal3" });
                context.SaveChanges();
                var controller = new AnimalRacesController(context);
                var result = await controller.DeleteConfirmed(9);
                Assert.IsType<RedirectToActionResult>(result);

            }
        }


    }
}
