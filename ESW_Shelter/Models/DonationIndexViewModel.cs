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
        public List<Donation> Donation;
        public List<DonationProduct> DonationProducts;
        public List<Product> Products;
        public List<Product> SelectedProducts;
        public Donation EditDonation;
        public Microsoft.AspNetCore.Mvc.Rendering.SelectList AnimalTypes;
        public Microsoft.AspNetCore.Mvc.Rendering.SelectList ProductTypes;
        public Microsoft.AspNetCore.Mvc.Rendering.SelectList UsersNames;
        public string AnimalType { get; set; }
        public string ProductType { get; set; }
        public string SearchString { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data do Donativo")]
        public DateTime DateOfDonation { get; set; }
        public int DonationID { get; set; }
        [Required(ErrorMessage = "Escolha um cliente para a doação!")]
        [Display(Prompt = "Examplo: José", Name = "Cliente")]
        public int UsersFK { get; set; }

    }
}
