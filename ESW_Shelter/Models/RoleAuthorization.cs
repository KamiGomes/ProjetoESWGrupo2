
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESW_Shelter.Models
{
    public class RoleAuthorization
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleAuthorizationID { get; set; }

        [ForeignKey("Role")]
        [Required(ErrorMessage = "Escolha uma função para dar autorizações!")]
        [Display(Name = "Autorização")]
        public int RoleFK { get; set; }

        [ForeignKey("Component")]
        [Required(ErrorMessage = "Escolha um componente para dar permissões!")]
        [Display(Name = "Componente")]
        public int ComponentFK { get; set; }

        [Display(Name = "Pode criar dados?")]
        public bool Create { get; set; }

        [Display(Name = "Pode ver os Dados?")]
        public bool Read { get; set; }

        [Display(Name = "Pode editar dados?")]
        public bool Update { get; set; }

        [Display(Name = "Pode eliminar dados?")]
        public bool Delete { get; set; }
    }
}
