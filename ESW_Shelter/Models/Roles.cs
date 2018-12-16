using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ESW_Shelter.Models
{
    public class Roles
    {
        [Key]
        [Required(ErrorMessage = "Id Required")]
        public int RoleID { get; set; }

        [Required(ErrorMessage = "Role Name Required")]
        [Display(Prompt = "Examplo: Funcionário, Administrador", Name = "Nome")]
        public string RoleName { get; set; }
    }
}
