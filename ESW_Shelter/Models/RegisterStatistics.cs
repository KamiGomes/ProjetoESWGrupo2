using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESW_Shelter.Models
{
    public class RegisterStatistics
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RegisterStatisticID { get; set; }

        [Required]
        public DateTime DateStatistic { get; set; }

        [Required]
        public int Count { get; set; }
    }
}
