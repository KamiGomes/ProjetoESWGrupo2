﻿
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace ESW_Shelter.Models
{
    public class Users// : IValidatableObject
    {
        /* ^[\w\s.,:;!?€¥£¢$-]{0,2048}$
            ^ -- Beginning of string/line
            [] -- A character class
            \w -- A word character
            \s -- A space character
            .,:;!?€¥£¢$- -- Punctuation and special characters
            {} -- Number of repeats (min,max)
            $ -- End of string/line */

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        [StringLength(256, ErrorMessage = "Email não pode ter mais que 256 caracteres!", MinimumLength = 1)]
        [Required(ErrorMessage = "Email em falta!")]
        [EmailAddress(ErrorMessage = "Formato de email incorreto!")]
        [Display(Prompt = "Examplo: example@domain.com", Name = "Email")]
        public String Email { get; set; }

        [StringLength(256, ErrorMessage = "Nome não pode ter mais que 256 caracteres!", MinimumLength = 1)]
        [Required(ErrorMessage = "Nome em falta!")]
        //[RegularExpression(@"^[a-zA-Z\s][\w~^´`]+$", ErrorMessage = "Por favor, utilize só caracteres!")]
        [Display(Prompt = "Examplo: John Wick", Name = "Nome")]
        public String Name { get; set; }

        [StringLength(12, ErrorMessage = "Siga o exemplo disponivel!Tamanho de Password: 6-12 caracteres!", MinimumLength = 6)]
        [Required(ErrorMessage = "Password em falta!")]
        [DataType(DataType.Password)]
        [Display(Prompt = "Examplo: John_152 (Min: 6 - Max: 12)", Name = "Password")]
        public String Password { get; set; }

        [Required]
        public Boolean ConfirmedEmail { get; set; }

        /* Info que só aparece no perfil ou então um administrador a criar */

        [Display(Prompt = "Examplo: Rua de José, nº 4, 1º esq", Name = "Morada")]
        [StringLength(100, ErrorMessage = "Morada não pode ter mais de 100 caracteres!", MinimumLength = 1)]
        public String Street { get; set; }

        [StringLength(10, ErrorMessage = "Código-Postal não pode ter mais de 10 caracteres!", MinimumLength = 5)]
        //[RegularExpression(ErrorMessage = "Por favor, siga o formato do exemplo!")]
        [Display(Prompt = "Examplo: 4000-010", Name = "Código-Postal")]
        public String PostalCode { get; set; }

        [Display(Prompt = "Examplo: Lisboa", Name = "Cidade")]
        [RegularExpression(@"^[a-zA-Z\s][\w~^´`]+$", ErrorMessage = "Por favor, utilize só caracteres!")]
        [StringLength(40, ErrorMessage = "Cidade não pode ter mais de 10 caracteres!", MinimumLength = 1)]
        public String City { get; set; }

        [RegularExpression(@"^(\d{9})$", ErrorMessage = "Formato de Telemóvel errado!")]
        [Display(Prompt = "Examplo: 910000000", Name = "Telemóvel")]
        public string Phone { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data de Nascimento")]
        public DateTime DateOfBirth { get; set; }

        [ForeignKey("Roles")]
        [Required(ErrorMessage = "Role Required!")]
        [Display(Name = "Role")]
        public int RoleID { get; set; }

        [NotMapped]
        [Display(Name = "Permissão")]
        public String RoleName { get; set; }

        [DataType("String")]
        public string CustomerId { get; set; }

        [NotMapped]
        public DateTime fixDateOfBirth { get; set; }
    }

}