using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ESW_Shelter.Models
{
    public class AnimalType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AnimalTypeID { get; set; }

        [StringLength(256, ErrorMessage = "Nome não pode ter mais que 256 caracteres!", MinimumLength = 1)]
        [Required(ErrorMessage = "Nome em falta!")]
        [RegularExpression(@"^[a-zA-Z\s][\w~^´`]+$", ErrorMessage = "Por favor, utilize só caracteres!")]
        [Display(Prompt = "Examplo: Cão, Cachorro, Gato, etc", Name = "Nome")]
        public String Name { get; set; }
    }
}
