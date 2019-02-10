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

namespace RolesControllerTests
{
    public class RolesControllerTests
    {

        private RolesController repository;
        public static DbContextOptions<ShelterContext> options { get; }
        public static string connectionString = "Server=(localdb)\\mssqllocaldb;Database=ShelterDB;Trusted_Connection=True;MultipleActiveResultSets=true";

        static RolesControllerTests()
        {
            options = new DbContextOptionsBuilder<ShelterContext>()
                .UseSqlServer(connectionString).Options;

        }

        [Fact]
        public async Task Index_CanFIndRoles_FromContext_NullParameters()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                context.Roles.AddRange(
                    new Roles {RoleName="asdasd",ComponentFK=1  },
                    new Roles { RoleName = "asdasd", ComponentFK = 1 });

                context.SaveChanges();
                var controller = new RolesController(context);
                var result = await controller.Index();

                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<IEnumerable<Roles>>(viewResult.ViewData.Model);
                Assert.IsType<ViewResult>(result);

                //Assert.IsType<List<AnimalType>>(result);
                //var viewResult = Assert.IsType<ViewResult>(result);
                //var model = Assert.IsAssignableFrom<AnimalType>(
                //    viewResult.ViewData.Model);
                //Assert.IsType<List<AnimalType>>(model);
            }
        }

        [Fact]
        public async Task DetailsRoles_GetForm_WhenIdIsDiferentFromModelId()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                var controller = new RolesController(context);
                var result = await controller.Details(null);

                Assert.IsType<NotFoundResult>(result);
            }
        }

        [Fact]
        public async Task DetailsRoles_GetForm_WhenIdDoesNotExist()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                var controller = new RolesController(context);
                var result = await controller.Details(0);

                Assert.IsType<NotFoundResult>(result);
            }
        }

        [Fact]
        public async Task DetailsRoles_GetForm_Sucess()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                var controller = new RolesController(context);
                var result = await controller.Details(4);

                Assert.IsType<ViewResult>(result);
            }
        }

        [Fact]
        public void Create_CanCallForm()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                var controller = new RolesController(context);
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
                var controller = new RolesController(context);
                controller.ModelState.AddModelError("error", "some error");
                var result = await controller.Create(new Roles { RoleName = "" });

                Assert.IsType<ViewResult>(result);

            }

        }

        [Fact]
        public async Task CreateRoles_CreateSucess()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                var controller = new RolesController(context);
                controller.ModelState.AddModelError("error", "some error");

                var result = await controller.Create(new Roles { RoleName = "ss" });

                Assert.IsType<ViewResult>(result);
            }
        }

        [Fact]
        public async Task EditRoles_GetForm_WhenIdIsInvalid()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                var controller = new RolesController(context);
                var result = await controller.Edit(null);

                Assert.IsType<NotFoundResult>(result);
            }
        }

        [Fact]
        public async Task EditRoles_GetForm_WhenIdIsNotFound()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                var controller = new RolesController(context);
                var result = await controller.Edit(0);

                Assert.IsType<NotFoundResult>(result);
            }
        }

        [Fact]
        public async Task EditRoles_GetForm_Sucess()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                var controller = new RolesController(context);
                var result = await controller.Edit(4);

                Assert.IsType<ViewResult>(result);
            }
        }


        [Fact]
        public async Task EditRoles_PostForm_GivenDiferentIdFromModelId()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                var controller = new RolesController(context);
                var result = await controller.Edit(3, new Roles() { RoleID = 2, RoleName= "" });

                Assert.IsType<NotFoundResult>(result);
            }
        }

        [Fact]
        public async Task EditRoles_PostForm_GivenInvalidValues()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                var controller = new RolesController(context);
                var result = await controller.Edit(4, new Roles() { RoleID = 4, RoleName = "" });

                Assert.IsType<RedirectToActionResult>(result);
            }
        }


        [Fact]
        public async Task EditRoles_PostForm_Sucess()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                var controller = new RolesController(context);
                var result = await controller.Edit(4, new Roles() { RoleID = 4, RoleName = "asdasd" });

                Assert.IsType<ViewResult>(result);
            }
        }


        [Fact]
        public async Task DeleteRoles_GetForm_InvalidID()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                context.Roles.AddRange(
                   new Roles {  RoleName = "asdasd" },
                   new Roles{  RoleName = "asdasd" });
                context.SaveChanges();
                var controller = new RolesController(context);
                var result = await controller.Delete(null);
                Assert.IsType<NotFoundResult>(result);

            }
        }

        [Fact]
        public async Task DeleteRoles_GetForm_NotFoundId()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                context.Roles.AddRange(
                   new Roles { RoleName = "asdasd" },
                   new Roles { RoleName = "asdasd" });
                context.SaveChanges();
                var controller = new RolesController(context);
                var result = await controller.Delete(0);
                Assert.IsType<NotFoundResult>(result);

            }
        }


        [Fact]
        public async Task DeleteRoles_GetForm_Sucess()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                context.Roles.AddRange(
                   new Roles { RoleName = "asdasd" },
                   new Roles { RoleName = "asdasd" });
                context.SaveChanges();
                var controller = new RolesController(context);
                var result = await controller.Delete(1003);
                Assert.IsType<ViewResult>(result);

            }
        }

        [Fact]
        public async Task DeleteConfirmationRoles_Sucesseful()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                context.Roles.AddRange(
                    new Roles { RoleName = "asdasd" },
                   new Roles { RoleName = "asdasd" });
                context.SaveChanges();
                var controller = new RolesController(context);
                var result = await controller.DeleteConfirmed(1004);
                Assert.IsType<RedirectToActionResult>(result);

            }
        }



    }
}
