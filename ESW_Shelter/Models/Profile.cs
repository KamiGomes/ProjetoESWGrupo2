﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESW_Shelter.Models
{
    public class Profile
    {
        [ForeignKey("Users")]
        [Required(ErrorMessage = "User Required!")]
        public int UserID { get; set; }

        public int UserInfoID { get; set; }
        [StringLength(256, ErrorMessage = "Email não pode ter mais que 256 caracteres!", MinimumLength = 1)]
        [Required(ErrorMessage = "Email em falta!")]
        [EmailAddress(ErrorMessage = "Formato de email incorreto!")]
        [Display(Prompt = "Exemplo: example@domain.com")]
        public String Email { get; set; }

        [StringLength(256, ErrorMessage = "Nome não pode ter mais que 256 caracteres!", MinimumLength = 1)]
        [Required(ErrorMessage = "Nome em falta!")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Por favor, utilize só caracteres!")]
        [Display(Prompt = "Exemplo: John Wick", Name = "Nome")]
        public String Name { get; set; }

        [StringLength(12, ErrorMessage = "Password não pode ter mais que 12 caracteres e menos de 6!", MinimumLength = 6)]
        [Required(ErrorMessage = "Password em falta!")]
        [DataType(DataType.Password)]
        [Display(Prompt = "Exemplo: John_152 (Min: 6 - Max: 12)")]
        public String Password { get; set; }

        [Required]
        public Boolean ConfirmedEmail { get; set; }

        [Display(Prompt = "Exemplo: Rua de José, nº 4, 1º esq", Name = "Morada")]
        [StringLength(100, ErrorMessage = "Morada não pode ter mais de 100 caracteres!", MinimumLength = 1)]
        public String Street { get; set; }

        [StringLength(10, ErrorMessage = "Código-Postal não pode ter mais de 10 caracteres!", MinimumLength = 5)]
        [Display(Prompt = "Exemplo: 4000-010", Name = "Código-Postal")]
        public String PostalCode { get; set; }

        [Display(Prompt = "Exemplo: Lisboa", Name = "Cidade")]
        [StringLength(40, ErrorMessage = "Cidade não pode ter mais de 10 caracteres!", MinimumLength = 1)]
        public String City { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^[0-9]{9,12}$", ErrorMessage = "Telemóvel têm de ter no minimo 9 números, e máximo 12!")]
        [Display(Prompt = "Exemplo: 910000000", Name = "Telemóvel")]
        public int Phone { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^[0-9]{9,12}$", ErrorMessage = "Contacto Alternativo têm de ter no minimo 9 números, e máximo 12!")]
        [Display(Prompt = "Exemplo: 910000000", Name = "Contacto Alternativo")]
        public int AlternativePhone { get; set; }

        [StringLength(256, ErrorMessage = "Facebook não pode ter mais de 256 caracteres!", MinimumLength = 1)]
        [EmailAddress(ErrorMessage = "Formato de email incorreto!")]
        [Display(Prompt = "Exemplo: example@domain.com", Name = "Email Alternativo")]
        public String AlternativeEmail { get; set; }

        [StringLength(150, ErrorMessage = "Facebook não pode ter mais de 150 caracteres!", MinimumLength = 1)]
        [Display(Prompt = "Copie o link aqui!")]
        public String Facebook { get; set; }

        [StringLength(150, ErrorMessage = "Twitter não pode ter mais de 150 caracteres!", MinimumLength = 1)]
        [Display(Prompt = "Copie o link aqui!")]
        public String Twitter { get; set; }

        [StringLength(150, ErrorMessage = "Instagram não pode ter mais de 150 caracteres!", MinimumLength = 1)]
        [Display(Prompt = "Copie o link aqui!")]
        public String Instagram { get; set; }

        [StringLength(150, ErrorMessage = "Tumblr não pode ter mais de 150 caracteres!", MinimumLength = 1)]
        [Display(Prompt = "Copie o link aqui!")]
        public String Tumblr { get; set; }

        [StringLength(150, ErrorMessage = "Website não pode ter mais de 150 caracteres!", MinimumLength = 1)]
        [Display(Prompt = "Copie o link aqui!")]
        public String Website { get; set; }

        [Required(ErrorMessage = "Role Required!")]
        public int RoleID { get; set; }
    }
}
