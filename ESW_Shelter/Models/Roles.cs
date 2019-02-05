using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ESW_Shelter.Models
{
    public class Roles
    {
        public const string administrator = "administrator";
        [Key]
        [Required(ErrorMessage = "Id Required")]
        public int RoleID { get; set; }

        [Required(ErrorMessage = "Role Name Required")]
        [Display(Prompt = "Examplo: Funcionário, Administrador", Name = "Nome")]
        public string RoleName { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Escolha um componente para dar permissões!")]
        [Display(Name = "Componente")]
        public int ComponentFK { get; set; }

        [NotMapped]
        [Display(Name = "Pode criar dados?")]
        public bool Create { get; set; }

        [NotMapped]
        [Display(Name = "Pode ver os Dados?")]
        public bool Read { get; set; }

        [NotMapped]
        [Display(Name = "Pode editar dados?")]
        public bool Update { get; set; }

        [NotMapped]
        [Display(Name = "Pode eliminar dados?")]
        public bool Delete { get; set; }
    }
}
