using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ESW_Shelter.Models
{
    public class AnimalProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AnimalProductID { get; set; }

        [ForeignKey("Animal")]
        public int AnimalFK { get; set; }

        [ForeignKey("Product")]
        public int ProductFK { get; set; }
    }
}
