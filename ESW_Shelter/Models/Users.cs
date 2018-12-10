using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ESW_Shelter.Models
{
    public class Users// : IValidatableObject
    {
        /* public string Honeypot { get; set; }

         public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
         {
             if (!String.IsNullOrEmpty(this.Honeypot))
             {
                 return new[] { new ValidationResult("An error occured") };
             }
             return null;
         }*/

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        [StringLength(256, ErrorMessage = "Email não pode ter mais que 256 caracteres!", MinimumLength = 1)]
        [Required(ErrorMessage = "Email em falta!")]
        [EmailAddress(ErrorMessage = "Formato de email incorreto!")]
        [Display(Prompt = "Examplo: example@domain.com")]
        public String Email { get; set; }

        [StringLength(256, ErrorMessage = "Nome não pode ter mais que 256 caracteres!", MinimumLength = 1)]
        [Required(ErrorMessage = "Nome em falta!")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Por favor, utilize só caracteres!")]
        [Display(Prompt = "Examplo: John Wick", Name = "Nome")]
        public String Name { get; set; }

        [StringLength(12, ErrorMessage = "Password não pode ter mais que 12 caracteres e menos de 6!", MinimumLength = 6)]
        [Required(ErrorMessage = "Password em falta!")]
        [DataType(DataType.Password)]
        [Display(Prompt = "Examplo: John_152 (Min: 6 - Max: 12)")]
        public String Password { get; set; }

        [Required]
        public Boolean ConfirmedEmail { get; set; }

        [ForeignKey("Roles")]
        [Required(ErrorMessage = "Role Required!")]
        public int RoleID { get; set; }
    }

}