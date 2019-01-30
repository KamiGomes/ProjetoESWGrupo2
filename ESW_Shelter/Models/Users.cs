﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ESW_Shelter.Models
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
        [StringLength(256, ErrorMessage = "Name cannot be longer than 256 characters!.", MinimumLength = 1)]
        [Required(ErrorMessage = "Missing email!")]
        [EmailAddress(ErrorMessage = "This is not an Email Address!")]
        [Display(Prompt = "Example: example@domain.com")]
        public String Email { get; set; }
        [StringLength(256, ErrorMessage = "Name cannot be longer than 256 characters!.", MinimumLength = 1)]
        [Required(ErrorMessage = "Missing Name!")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Please only use leters!")]
        [Display(Prompt = "Example: John Wick")]
        public String Name { get; set; }
        [Required(ErrorMessage = "Missing Password!")]
        [StringLength(12, ErrorMessage = "Name cannot be longer than 12 characters and shorter than 6!.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Prompt = "Example: Jon_152 (Min: 6 - Max: 12)")]
        public String Password { get; set; }
        [Required]
        public Boolean ConfirmedEmail { get; set; }

        [ForeignKey("Roles")]
        [Required(ErrorMessage = "Role Required!")]
        public int RoleID { get; set; }
    }

}
