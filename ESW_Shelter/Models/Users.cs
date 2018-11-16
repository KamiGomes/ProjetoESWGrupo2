using Microsoft.EntityFrameworkCore;
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
        [Required(ErrorMessage = "Missing email!")]
        public String Email { get; set; }
        [Required(ErrorMessage = "Missing Name!")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please only use leters!")]
        public String Name { get; set; }
        [Required(ErrorMessage = "Missing Password!")]
        //[Range(8, 12, ErrorMessage = "Password must contain between 8 to 12 characters!")]
        public String Password { get; set; }
    }

}
