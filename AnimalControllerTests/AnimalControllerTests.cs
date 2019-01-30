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
using Microsoft.AspNetCore.Hosting;

namespace AnimalControllerTests
{
    public class AnimalControllerTests
    {
        private AnimalsController repository;
        public static DbContextOptions<ShelterContext> options { get; }
        public static string connectionString = "Server=(localdb)\\mssqllocaldb;Database=ShelterDB;Trusted_Connection=True;MultipleActiveResultSets=true";

        static AnimalControllerTests()
        {
            options = new DbContextOptionsBuilder<ShelterContext>()
                .UseSqlServer(connectionString).Options;

        }

        /*[Fact]
        public async Task Index_CanFIndAnimals_FromContext_NullParameters()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                context.Animal.AddRange(
                    new Animal { Name = "animal3", DateOfBirth = DateTime.MinValue, Disinfection = true, Neutered = true, Description = "sasdsad", AnimalRaceFK = 2 },
                    new Animal { Name = "animal3", DateOfBirth = DateTime.MinValue, Disinfection = true, Neutered = true, Description = "sasdsad", AnimalRaceFK = 2 },
                    new Animal { Name = "animal3" ,DateOfBirth =DateTime.MinValue ,Disinfection=true, Neutered= true, Description="sasdsad", AnimalRaceFK=2});

                context.SaveChanges();
                var controller = new AnimalsController(context);
                var result = await controller.Index("","",false,false,"");

                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<Animal>(viewResult.ViewData.Model);
                Assert.IsType<ViewResult>(result);

                //Assert.IsType<List<AnimalType>>(result);
                //var viewResult = Assert.IsType<ViewResult>(result);
                //var model = Assert.IsAssignableFrom<AnimalType>(
                //    viewResult.ViewData.Model);
                //Assert.IsType<List<AnimalType>>(model);
            }
        }*/

        [Fact]
        public void Create_CanCallForm()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                var controller = new AnimalsController(context);
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
                var controller = new AnimalsController(context);
                controller.ModelState.AddModelError("error", "some error");
                var result = await controller.Create(animal: null);

                Assert.IsType<ViewResult>(result);

            }

        }

        [Fact]
        public async Task CreateAnimals_CreateSucess()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                var controller = new AnimalsController(context);
                controller.ModelState.AddModelError("error", "some error");

                var result = await controller.Create(new Animal { Name = "sas", DateOfBirth = DateTime.MinValue, Disinfection = true, Neutered = true, Description = "sasdsad", AnimalRaceFK = 2 });

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
        public async Task EditAnimals_PostForm_GivenInvalidModel()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                var controller = new AnimalsController(context);
                var result = await controller.Edit(2, new Animal() { AnimalID = 2, Name = "", DateOfBirth = DateTime.MinValue, Disinfection = true, Neutered = true, Description = "sasdsad", AnimalRaceFK = 2 });

                Assert.IsType<NotFoundResult>(result);
            }
        }

        [Fact]
        public async Task EditAnimals_PostForm_ValuesNotValid()
        {
            using (var context = new ShelterContext(options))
            {

                context.Database.EnsureCreated();
                var controller = new AnimalsController(context);
                var result = await controller.Edit(1, new Animal { Name = "" });

                Assert.IsType<NotFoundResult>(result);
            }
        }

        [Fact]
        public async Task EditAnimals_PostForm_DifferentIdFromGivenId()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                var controller = new AnimalsController(context);
                var result = await controller.Edit(2, new Animal { AnimalID = 3, Name = "", DateOfBirth = DateTime.MinValue, Disinfection = true, Neutered = true, Description = "sasdsad", AnimalRaceFK = 2 });

                Assert.IsType<NotFoundResult>(result);
            }
        }

        [Fact]
        public async Task EditAnimals_PostForm_Sussess()
        {
            using (var context = new ShelterContext(options))
            {
                
                var controller = new AnimalsController(context);
                var result = await controller.Edit(10, new Animal() { AnimalID = 10, Name = "", DateOfBirth = DateTime.MinValue, Disinfection = true, Neutered = true, Description = "sasdsad", AnimalRaceFK = 2 });

                Assert.IsType<RedirectToActionResult>(result);
            }
        }

        [Fact]
        public async Task DeleteAnimals_GetForm_InvalidID()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                context.Animal.AddRange(
                   new Animal { Name = "animal1", DateOfBirth = DateTime.MinValue, Disinfection = true, Neutered = true, Description = "sasdsad", AnimalRaceFK = 2 },
                    new Animal { Name = "animal1", DateOfBirth = DateTime.MinValue, Disinfection = true, Neutered = true, Description = "sasdsad", AnimalRaceFK = 2 },
                    new Animal { Name = "animal1", DateOfBirth = DateTime.MinValue, Disinfection = true, Neutered = true, Description = "sasdsad", AnimalRaceFK = 2 });
                context.SaveChanges();
                var controller = new AnimalsController(context);
                var result = await controller.Delete(0);
                Assert.IsType<NotFoundResult>(result);

            }
        }

        [Fact]
        public async Task DeleteAnimals_GetForm_InvalidModel()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                context.Animal.AddRange(
                   new Animal { Name = "animal1", DateOfBirth = DateTime.MinValue, Disinfection = true, Neutered = true, Description = "sasdsad", AnimalRaceFK = 2 },
                    new Animal { Name = "animal1", DateOfBirth = DateTime.MinValue, Disinfection = true, Neutered = true, Description = "sasdsad", AnimalRaceFK = 2 },
                    new Animal { Name = "animal1", DateOfBirth = DateTime.MinValue, Disinfection = true, Neutered = true, Description = "sasdsad", AnimalRaceFK = 2 });
                context.SaveChanges();
                var controller = new AnimalsController(context);
                var result = await controller.Delete(9999999);
                Assert.IsType<NotFoundResult>(result);

            }
        }

        [Fact]
        public async Task DeleteAnimals_GetForm_FoundAnimal()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                context.Animal.AddRange(
                   new Animal { Name = "animal1", DateOfBirth = DateTime.MinValue, Disinfection = true, Neutered = true, Description = "sasdsad", AnimalRaceFK = 2 },
                    new Animal { Name = "animal1", DateOfBirth = DateTime.MinValue, Disinfection = true, Neutered = true, Description = "sasdsad", AnimalRaceFK = 2 },
                    new Animal { Name = "animal1", DateOfBirth = DateTime.MinValue, Disinfection = true, Neutered = true, Description = "sasdsad", AnimalRaceFK = 2 });
                context.SaveChanges();
                var controller = new AnimalsController(context);
                var result = await controller.Delete(8);
                Assert.IsType<ViewResult>(result);

            }
        }

        [Fact]
        public async Task DeleteConfirmationAnimals_Sucesseful()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                context.Animal.AddRange(
                    new Animal { Name = "animal1", DateOfBirth = DateTime.MinValue, Disinfection = true, Neutered = true, Description = "sasdsad", AnimalRaceFK = 2 },
                    new Animal { Name = "animal1", DateOfBirth = DateTime.MinValue, Disinfection = true, Neutered = true, Description = "sasdsad", AnimalRaceFK = 2 },
                    new Animal { Name = "animal1", DateOfBirth = DateTime.MinValue, Disinfection = true, Neutered = true, Description = "sasdsad", AnimalRaceFK = 2 });
                context.SaveChanges();
                var controller = new AnimalsController(context);
                var result = await controller.DeleteConfirmed(10);
                Assert.IsType<RedirectToActionResult>(result);

            }
        }

    }
}
