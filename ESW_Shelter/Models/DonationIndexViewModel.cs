using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ESW_Shelter.Models
{
    public class DonationIndexViewModel
    {

        //All Donations
        public List<Donation> Donation;
        //Donation Products
        public List<DonationProduct> DonationProducts;
        //All Products
        public List<Product> Products;
        //To save Selected Items
        public List<Product> SelectedProducts;
        //Item to Edit
        public Donation EditDonation;

        public Microsoft.AspNetCore.Mvc.Rendering.SelectList AnimalTypes;
        public Microsoft.AspNetCore.Mvc.Rendering.SelectList ProductTypes;
        public Microsoft.AspNetCore.Mvc.Rendering.SelectList UsersNames;
        //Search Items
        public string AnimalType { get; set; }
        public string ProductType { get; set; }
        public string SearchString { get; set; }
        public DateTime DateString { get; set; }
        public string ClientString { get; set; }

        //Create Donation
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Escolha uma data para a doação!")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data do Donativo")]
        public DateTime DateOfDonation { get; set; }

        public int DonationID { get; set; }

        [Required(ErrorMessage = "Escolha um cliente para a doação!")]
        [Display(Prompt = "Examplo: José", Name = "Cliente")]
        public int UsersFK { get; set; }

    }
}
