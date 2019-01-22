using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESW_Shelter.Models
{
    public class AnimalUsers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AnimalUsersID { get; set; }

        [ForeignKey("Animal")]
        [Required]
        public int AnimalFK { get; set; }

        [ForeignKey("Users")]
        [Required]
        public int UsersFK { get; set; }
    }
}
