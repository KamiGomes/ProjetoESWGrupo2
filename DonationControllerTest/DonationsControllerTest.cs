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

namespace DonationControllerTest

{
    public class DonationsControllerTests
    {
        private DonationsController repository;
        public static DbContextOptions<ShelterContext> options { get; }
        public static string connectionString = "Server=(localdb)\\mssqllocaldb;Database=ShelterDB;Trusted_Connection=True;MultipleActiveResultSets=true";

        static DonationsControllerTests()
        {
            options = new DbContextOptionsBuilder<ShelterContext>()
                .UseSqlServer(connectionString).Options;

        }
        /**
            Teste do Index para verificar que consegue devolver o model (Parametros de pesquisa a null) 
        */
        [Fact]
        public async Task Index_CanFIndDonation_FromContext_NullParameters()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                context.Donation.AddRange(
                    new Donation { UsersFK = 2, DateOfDonation = DateTime.MinValue, ProductName = "", UsersName = "" },
                    new Donation { UsersFK = 2, DateOfDonation = DateTime.MinValue, ProductName = "", UsersName = "" },
                    new Donation { UsersFK = 2, DateOfDonation = DateTime.MinValue, ProductName = "", UsersName = "" });
                context.DonationProduct.AddRange(
                    new DonationProduct { Quantity = 10, DonationFK = 1, ProductFK = 1 },
                    new DonationProduct { Quantity = 15, DonationFK = 2, ProductFK = 1 },
                    new DonationProduct { Quantity = 1, DonationFK = 3, ProductFK = 1 });
                context.SaveChanges();
                var controller = new DonationsController(context);
                var result = await controller.Index("","","","", DateTime.MinValue);

                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<DonationIndexViewModel>(
                    viewResult.ViewData.Model);
                Assert.IsType<DonationIndexViewModel>(model);
            }
        }

        /**
            Teste do Index para verificar que consegue devolver o modelo (Parametro de pesquisa - Cliente ) 
        */
        [Fact]
        public async Task Index_CanFIndDonation_FromContext_Client()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                context.Donation.AddRange(
                    new Donation { UsersFK = 2, DateOfDonation = DateTime.MinValue, ProductName = "", UsersName = "" },
                    new Donation { UsersFK = 2, DateOfDonation = DateTime.MinValue, ProductName = "", UsersName = "" },
                    new Donation { UsersFK = 2, DateOfDonation = DateTime.MinValue, ProductName = "", UsersName = "" });
                context.DonationProduct.AddRange(
                    new DonationProduct { Quantity = 10, DonationFK = 1, ProductFK = 1 },
                    new DonationProduct { Quantity = 15, DonationFK = 2, ProductFK = 1 },
                    new DonationProduct { Quantity = 1, DonationFK = 3, ProductFK = 1 });
                var controller = new DonationsController(context);
                var result = await controller.Index("Miguel","Cao","Acessorios","Admin", DateTime.MinValue);

                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<DonationIndexViewModel>(viewResult.ViewData.Model);
                Assert.IsType<DonationIndexViewModel>(model);
            }
        }

        /**
            Teste do Index para verificar que consegue devolver o modelo (Parametro de pesquisa - Data de Doação)
        */
        [Fact]
        public async Task Index_CanFIndDonation_FromContext_DateOfDonation()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                context.Donation.AddRange(
                    new Donation { UsersFK = 2, DateOfDonation = DateTime.Now, ProductName = "", UsersName = "" },
                    new Donation { UsersFK = 2, DateOfDonation = DateTime.Now, ProductName = "", UsersName = "" },
                    new Donation { UsersFK = 2, DateOfDonation = DateTime.Now, ProductName = "", UsersName = "" });
                context.DonationProduct.AddRange(
                    new DonationProduct { Quantity = 10, DonationFK = 1, ProductFK = 1 },
                    new DonationProduct { Quantity = 15, DonationFK = 2, ProductFK = 1 },
                    new DonationProduct { Quantity = 1, DonationFK = 3, ProductFK = 1 });
                var controller = new DonationsController(context);
                var result = await controller.Index("","","","", DateTime.Now);

                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<DonationIndexViewModel>(viewResult.ViewData.Model);
                Assert.IsType<DonationIndexViewModel>(model);
            }
        }

        /**
            Teste do Index para verificar que consegue devolver o modelo (Parametro de pesquisa - Cliente e Data de Doação)
        */
        [Fact]
        public async Task Index_CanFIndDonation_FromContext_DateOfDonationAndClient()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                context.Donation.AddRange(
                    new Donation { UsersFK = 2, DateOfDonation = DateTime.Now, ProductName = "", UsersName = "" },
                    new Donation { UsersFK = 2, DateOfDonation = DateTime.Now, ProductName = "", UsersName = "" },
                    new Donation { UsersFK = 2, DateOfDonation = DateTime.Now, ProductName = "", UsersName = "" });
                context.DonationProduct.AddRange(
                    new DonationProduct { Quantity = 10, DonationFK = 1, ProductFK = 1 },
                    new DonationProduct { Quantity = 15, DonationFK = 2, ProductFK = 1 },
                    new DonationProduct { Quantity = 1, DonationFK = 3, ProductFK = 1 });
                var controller = new DonationsController(context);
                var result = await controller.Index("Miguel", "Cao", "Acessorios", "Admin", DateTime.Now);

                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<DonationIndexViewModel>(viewResult.ViewData.Model);
                Assert.IsType<DonationIndexViewModel>(model);
            }
        }
        /*
            Teste de Create para verificar que consegue chamar a View
        */
        [Fact]
        public void Create_CanCallForm()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                var controller = new DonationsController(context);
                var result = controller.Create("", "", "");//Isto mais tarde é para se alterar
                                                           /*var viewResult = Assert.IsType<ViewResult>(result);
                                                           var model = Assert.IsAssignableFrom<DonationIndexViewModel>(viewResult.ViewData.Model);*/
                Assert.IsType<ViewResult>(result);
            }
        }

        /*
             Teste Create onde onde não consegue encontrar os dados pedidos ( UsersFK está vazio ou negativo ou não existe )
         */
        [Fact]
        public async Task CreateDonation_ReturnsToCreateIf_NoUsersFK()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                var controller = new DonationsController(context);
                controller.ModelState.AddModelError("Erro", "DateOfDonation invalido");

                var result = await controller.Create(new DonationIndexViewModel { DateOfDonation = DateTime.Now, UsersFK = -1 });

                Assert.IsType<RedirectResult>(result);
            }
        }

        /*
            Teste Create onde onde não consegue encontrar os dados pedidos ( Data de doação está vazio ou negativo ou não existe )
        */
        [Fact]
        public async Task CreateDonation_ReturnsNotFoundResult_NoDateOfDonation()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                var controller = new DonationsController(context);
                controller.ModelState.AddModelError("Erro", "DateOfDonation invalido");

                var result = await controller.Create(new DonationIndexViewModel { DonationID = 1005, DateOfDonation = DateTime.MinValue, UsersFK = 2, EditDonation = new Donation { DonationID = 1005 } });

                Assert.IsType<RedirectResult>(result);
            }
        }
        /*
            Teste Create onde é efetuado com sucesso
        */
        [Fact]
        public async Task CreateDonation_CreateSucess()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                var controller = new DonationsController(context);
                controller.ModelState.AddModelError("Erro", "DateOfDonation invalido");

                var result = await controller.Create(new DonationIndexViewModel { DateOfDonation = DateTime.Now, UsersFK = 2 });

                Assert.IsType<ViewResult>(result);
            }
        }
        /*
         Teste Edit onde não consegue encontrar (Id Errado)   
      */
        [Fact]
        public async Task EditDonation_ReturnsNotFoundResult_WhenIdIsDiferentFromModelId()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                var controller = new DonationsController(context);
                var result = await controller.Edit(99999999, "", "", "");

                Assert.IsType<NotFoundResult>(result);
            }
        }
        /*
          Teste Edit onde não consegue não edita ( UsersFK é inválida)
      */
        [Fact]
        public async Task EditDonation_ReturnsNotFoundResult_NoUsersFK()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                var controller = new DonationsController(context);
                controller.ModelState.AddModelError("Erro", "Texto erro");

                var result = await controller.Edit(1005, new DonationIndexViewModel { DonationID = 1005, DateOfDonation = DateTime.MinValue, UsersFK = -1, EditDonation = new Donation { DonationID = 1005 } });

                Assert.IsType<RedirectResult>(result);
            }
        }
        /*
             Teste Edit onde não consegue não edita ( DateOfDonation é inválida)
         */
        [Fact]
        public async Task EditDonation_ReturnsNotFoundResult_NoDateOfDonation()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                var controller = new DonationsController(context);
                controller.ModelState.AddModelError("Erro", "Texto erro");

                var result = await controller.Edit(1005, new DonationIndexViewModel { DonationID = 1005, DateOfDonation = DateTime.MinValue, UsersFK = 2, EditDonation = new Donation { DonationID = 1005 } });

                Assert.IsType<RedirectResult>(result);
            }
        }

        /*
     Teste Edit sucesso
 */
        [Fact]
        public async Task EditDonation_EditSucess()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                var controller = new DonationsController(context);
                controller.ModelState.AddModelError("Erro", "Texto erro");

                var result = await controller.Edit(1005, new DonationIndexViewModel { DonationID = 1005, DateOfDonation = DateTime.Now, UsersFK = 2, EditDonation = new Donation { DonationID = 1005 } });

                Assert.IsType<ViewResult>(result);
            }
        }

        /*
            Teste Delete onde não consegue encontrar o Donation ( Id Errado)
         */
        [Fact]
        public async Task DeleteDonation_InvalidID()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                context.Donation.AddRange(
                    new Donation { UsersFK = 2, DateOfDonation = DateTime.MinValue, ProductName = "", UsersName = "" },
                    new Donation { UsersFK = 2, DateOfDonation = DateTime.MinValue, ProductName = "", UsersName = "" },
                    new Donation { UsersFK = 2, DateOfDonation = DateTime.MinValue, ProductName = "", UsersName = "" });
                context.SaveChanges();
                var controller = new DonationsController(context);
                var result = await controller.Delete(0);
                Assert.IsType<NotFoundResult>(result);

            }
        }
        /*
            Teste Delete onde consegue encontrar o Donation
         */
        [Fact]
        public async Task DeleteDonation_FoundDonation()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                context.Donation.AddRange(
                    new Donation { UsersFK = 2, DateOfDonation = DateTime.Now, ProductName = "", UsersName = "" },
                    new Donation { UsersFK = 2, DateOfDonation = DateTime.Now, ProductName = "", UsersName = "" },
                    new Donation { UsersFK = 2, DateOfDonation = DateTime.Now, ProductName = "", UsersName = "" });
                context.DonationProduct.AddRange(
                    new DonationProduct { Quantity = 10, DonationFK = 1, ProductFK = 1 },
                    new DonationProduct { Quantity = 15, DonationFK = 2, ProductFK = 1 },
                    new DonationProduct { Quantity = 1, DonationFK = 3, ProductFK = 1 });
                var controller = new DonationsController(context);
                var result = await controller.Delete(1030);

                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<Donation>(viewResult.ViewData.Model);
                Assert.IsType<Donation>(model);
            }
        }
        /*
            Teste de DeleteConfirmed onde não consegue eliminar (Id errado)
         */
        [Fact]
        public async Task DeleteConfirmationDonation_InvalidID()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                context.Donation.AddRange(
                    new Donation { UsersFK = 2, DateOfDonation = DateTime.MinValue, ProductName = "", UsersName = "" },
                    new Donation { UsersFK = 2, DateOfDonation = DateTime.MinValue, ProductName = "", UsersName = "" },
                    new Donation { UsersFK = 2, DateOfDonation = DateTime.MinValue, ProductName = "", UsersName = "" });
                context.SaveChanges();
                var controller = new DonationsController(context);
                var result = await controller.DeleteConfirmed(0);
                Assert.IsType<NotFoundResult>(result);

            }
        }
        /*
            Teste de DeleteConfirmed sucesso
         */
        [Fact]
        public async Task DeleteConfirmationDonation_Sucess()
        {
            using (var context = new ShelterContext(options))
            {
                context.Database.EnsureCreated();
                context.Donation.AddRange(
                    new Donation { UsersFK = 2, DateOfDonation = DateTime.MinValue, ProductName = "", UsersName = "" },
                    new Donation { UsersFK = 2, DateOfDonation = DateTime.MinValue, ProductName = "", UsersName = "" },
                    new Donation { UsersFK = 2, DateOfDonation = DateTime.MinValue, ProductName = "", UsersName = "" });
                context.SaveChanges();
                var controller = new DonationsController(context);
                var result = await controller.DeleteConfirmed(1016);//
                Assert.IsType<RedirectToActionResult>(result);
            }
        }

    }
}