using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ESW_Shelter.Models
{
    public class DonationProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DonationProductID { get; set; }

        [ForeignKey("Donation")]
        [Required(ErrorMessage = "Escolha um Donativo!")]
        [Display(Prompt = "Examplo: Comida", Name = "Produto")]
        public int DonationFK { get; set; }

        [ForeignKey("Product")]
        [Required(ErrorMessage = "Escolha um produto!")]
        [Display(Prompt = "Examplo: Comida", Name = "Produto")]
        public int ProductFK { get; set; }

        [Required(ErrorMessage = "Quantidade em falta!")]
        [Range(0, int.MaxValue, ErrorMessage = "Por favor, insira só números inteiros!")]
        [Display(Prompt = "Examplo: 10", Name = "Quantidade")]
        public int Quantity { get; set; }
    }
}
