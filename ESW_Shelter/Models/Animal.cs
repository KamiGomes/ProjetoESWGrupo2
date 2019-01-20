using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ESW_Shelter.Models
{
    public class Animal
    {
        [StringLength(256, ErrorMessage = "Nome não pode ter mais que 256 caracteres!", MinimumLength = 1)]
        [Required(ErrorMessage = "Nome em falta!")]
        [Display(Prompt = "Examplo: Secos Continente", Name = "Nome")]
        public String Name { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AnimalId {get;set;}

        [ForeignKey("Users")]
        [null]
        public int ownerFK {get;set;}

        [NotMapped]
        public string OwnerNane{get;set;}

        [null]
        public string notes{get;set;}

        [ForeignKey("AnimalType")]
        [Required(ErrorMessage = "Escolha um tipo para este Animal!")]
        [Display(Prompt = "Examplo: Pato", Name = "Tipo de Animal")]
        public int AnimalTypeFK { get; set; }

        
        [NotMapped]
        public String AnimaltypeName { get; set; }

        public Bitmap foto {get;set;}
        /*
        [DataType(DataType.ImageUrl)]
        public String foto{get;set;}
         */
    }
}