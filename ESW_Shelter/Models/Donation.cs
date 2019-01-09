using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESW_Shelter.Models
{
    public class Donation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DonationID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data do Donativo")]
        public DateTime DateOfDonation { get; set; }

        [ForeignKey("Users")]
        [Required(ErrorMessage = "Escolha um produto!")]
        [Display(Prompt = "Examplo: José", Name = "Cliente")]
        public int UsersFK { get; set; }


        [NotMapped]
        public String UsersName { get; set; }

        [NotMapped]
        [Display(Prompt = "Examplo: José", Name = "Produtos Doados")]
        public String ProductName { get; set; }

    }
}
