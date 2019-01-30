using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ESW_Shelter.Models
{
    public class UsersInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserInfoID { get; set; }
        [StringLength(100, ErrorMessage = "Street name cannot have more than 100 characters!", MinimumLength = 1)]
        public String Street { get; set; }
        [StringLength(10, ErrorMessage = "Name cannot be longer than 10 characters!.", MinimumLength = 5)]
        public String PostalCode { get; set; }
        [StringLength(40, ErrorMessage = "Name cannot be longer than 40 characters!.", MinimumLength = 1)]
        public String City { get; set; }
        [Phone]
        public int Phone { get; set; }
        [Phone]
        public int AlternativePhone { get; set; }
        [EmailAddress]
        public String AlternativeEmail { get; set; }
        [StringLength(150, ErrorMessage = "Facebook cannot have more than 150 characters!", MinimumLength = 1)]
        public String Facebook { get; set; }
        [StringLength(150, ErrorMessage = "Twitter cannot have more than 150 characters!", MinimumLength = 1)]
        public String Twitter { get; set; }
        [StringLength(150, ErrorMessage = "Instagram cannot have more than 150 characters!", MinimumLength = 1)]
        public String Instagram { get; set; }
        [StringLength(150, ErrorMessage = "Tumblr cannot have more than 150 characters!", MinimumLength = 1)]
        public String Tumblr { get; set; }
        [StringLength(150, ErrorMessage = "Website cannot have more than 150 characters!", MinimumLength = 1)]
        public String Website { get; set; }
        [Required]
        public int UserID { get; set; }
        /*[ForeignKey("FK_UserInfoID_UserID")]
        public Users User { get; set; }*/
    }
}
