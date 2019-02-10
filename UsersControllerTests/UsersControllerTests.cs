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

namespace UsersControllerTests
{
   public class UsersControllerTests
    {
        private UsersController repository;
        public static DbContextOptions<ShelterContext> options { get; }
        public static string connectionString = "Server=(localdb)\\mssqllocaldb;Database=ShelterDB;Trusted_Connection=True;MultipleActiveResultSets=true";

        static UsersControllerTests()
        {
            options = new DbContextOptionsBuilder<ShelterContext>()
                .UseSqlServer(connectionString).Options;
        }


        [Fact]
        public void Create_CanCallForm()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                var controller = new UsersController(context);
                var result = controller.Create();
                Assert.IsType<ViewResult>(result);

            }
        }

        [Fact]
        public async Task Create_ReturnsBadRequest_GivenInvalidValues()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                var controller = new UsersController(context);
                controller.ModelState.AddModelError("error", "some error");
                var result = await controller.Create(new Users { Email = "", Name = "sadad", Password = "asddasd" });

                Assert.IsType<ViewResult>(result);

            }

        }

        [Fact]
        public async Task CreateUsers_CreateSucess()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                var controller = new UsersController(context);
                controller.ModelState.AddModelError("error", "some error");

                var result = await controller.Create(new Users { Email = "sas", Name = "sadad", Password = "asddasd" });

                Assert.IsType<ViewResult>(result);
            }
        }

        [Fact]
        public async Task EditUsers_GetForm_WhenIdIsDiferentFromModelId()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                var controller = new UsersController(context);
                var result = await controller.Edit(9999999);

                Assert.IsType<NotFoundResult>(result);
            }
        }

        [Fact]
        public async Task EditUsers_GetForm_SucessFetch()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                var controller = new UsersController(context);
                var result = await controller.Edit(1001);

                Assert.IsType<ViewResult>(result);
            }
        }


        [Fact]
        public async Task EditUsers_PostForm_WhenIdIsDiferentFromModelId()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                var controller = new UsersController(context);
                var result = await controller.Edit(1001, new Users() { UserID = 2, Email = "sas", Name = "sadad", Password = "asddasd" });

                Assert.IsType<NotFoundResult>(result);
            }
        }

        [Fact]
        public async Task EditUsers_PostForm_WhenValuesInvalid()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                var controller = new UsersController(context);
                var result = await controller.Edit(1001, new Users() { UserID =1001 , Email = "", Name = "sadad", Password = "asddasd" });

                Assert.IsType<ViewResult>(result);
            }
        }

        [Fact]
        public async Task EditUsers_PostForm_SucessfullEdit()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                var controller = new UsersController(context);
                var result = await controller.Edit(1001, new Users() { UserID = 1001, Email = "davidmfa@hotmail.com", Name = "Davidsadad", Password = "David-12" ,ConfirmedEmail=true});

                Assert.IsType<ViewResult>(result);
            }
        }

        [Fact]
        public async Task DeleteUsers_GetForm_InvalidID()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                context.Users.AddRange(
                    new Users {  Email = "davidmfa@hotmail.com", Name = "Davidsadad", Password = "David-12", ConfirmedEmail = true },
                    new Users { Email = "davidmfa1@hotmail.com", Name = "Davidsadad", Password = "David-12", ConfirmedEmail = true },
                    new Users { Email = "davidmfa2@hotmail.com", Name = "Davidsadad", Password = "David-12", ConfirmedEmail = true });
                context.SaveChanges();
                var controller = new UsersController(context);
                var result = await controller.Delete(null);
                Assert.IsType<NotFoundResult>(result);

            }
        }

        [Fact]
        public async Task DeleteUsers_GetForm_IDDoesNotExist()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                context.Users.AddRange(
                    new Users { Email = "davidmfa@hotmail.com", Name = "Davidsadad", Password = "David-12", ConfirmedEmail = true },
                    new Users { Email = "davidmfa1@hotmail.com", Name = "Davidsadad", Password = "David-12", ConfirmedEmail = true },
                    new Users { Email = "davidmfa2@hotmail.com", Name = "Davidsadad", Password = "David-12", ConfirmedEmail = true });
                context.SaveChanges();
                var controller = new UsersController(context);
                var result = await controller.Delete(0);
                Assert.IsType<NotFoundResult>(result);

            }
        }

        [Fact]
        public async Task DeleteUsers_GetForm_Sucess()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                context.Users.AddRange(
                    new Users { Email = "davidmfa@hotmail.com", Name = "Davidsadad", Password = "David-12", ConfirmedEmail = true },
                    new Users { Email = "davidmfa1@hotmail.com", Name = "Davidsadad", Password = "David-12", ConfirmedEmail = true },
                    new Users { Email = "davidmfa2@hotmail.com", Name = "Davidsadad", Password = "David-12", ConfirmedEmail = true });
                context.SaveChanges();
                var controller = new UsersController(context);
                var result = await controller.Delete(1001);
                Assert.IsType<ViewResult>(result);

            }
        }

        [Fact]
        public async Task DeleteUsers_PostForm_Sucess()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                context.Users.AddRange(
                    new Users { Email = "davidmfa@hotmail.com", Name = "Davidsadad", Password = "David-12", ConfirmedEmail = true },
                    new Users { Email = "davidmfa1@hotmail.com", Name = "Davidsadad", Password = "David-12", ConfirmedEmail = true },
                    new Users { Email = "davidmfa2@hotmail.com", Name = "Davidsadad", Password = "David-12", ConfirmedEmail = true });
                context.SaveChanges();
                var controller = new UsersController(context);
                var result = await controller.Delete(3007);
                Assert.IsType<ViewResult>(result);

            }
        }








    }
}
