using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ESW_Shelter.Models
{
    public class Profile
    {

        public int UserID { get; set; }

        public int UserInfoID { get; set; }

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
        [Display(Prompt = "Example: John_152 (Min: 6 - Max: 12)")]
        public String Password { get; set; }

        [Required]
        public Boolean ConfirmedEmail { get; set; }

        [Display(Prompt = "Example: Street of Streets, nº 4, 1º floor")]
        [StringLength(100, ErrorMessage = "Street name cannot have more than 100 characters!", MinimumLength = 1)]
        public String Street { get; set; }

        [StringLength(10, ErrorMessage = "Name cannot be longer than 10 characters!.", MinimumLength = 5)]
        [Display(Prompt = "Example: 4000-010")]
        public String PostalCode { get; set; }

        [Display(Prompt = "Example: Lisbon")]
        [StringLength(40, ErrorMessage = "Name cannot be longer than 40 characters!.", MinimumLength = 1)]
        public String City { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^[0-9]{9,12}$", ErrorMessage = "Phone must have atleast 9! Maximum 12!")]
        [Display(Prompt = "Example: 910000000")]
        public int Phone { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^[0-9]{9,12}$", ErrorMessage = "Phone must have atleast 9! Maximum 12!")]
        [Display(Prompt = "Example: 910000000")]
        public int AlternativePhone { get; set; }

        [StringLength(256, ErrorMessage = "Name cannot be longer than 256 characters!.", MinimumLength = 1)]
        [Required(ErrorMessage = "Missing email!")]
        [EmailAddress(ErrorMessage = "This is not an Email Address!")]
        [Display(Prompt = "Example: example@domain.com")]
        public String AlternativeEmail { get; set; }

        [StringLength(150, ErrorMessage = "Facebook cannot have more than 150 characters!", MinimumLength = 1)]
        [Display(Prompt = "Paste your link here!")]
        public String Facebook { get; set; }

        [StringLength(150, ErrorMessage = "Twitter cannot have more than 150 characters!", MinimumLength = 1)]
        [Display(Prompt = "Paste your link here!")]
        public String Twitter { get; set; }

        [StringLength(150, ErrorMessage = "Instagram cannot have more than 150 characters!", MinimumLength = 1)]
        [Display(Prompt = "Paste your link here!")]
        public String Instagram { get; set; }

        [StringLength(150, ErrorMessage = "Tumblr cannot have more than 150 characters!", MinimumLength = 1)]
        [Display(Prompt = "Paste your link here!")]
        public String Tumblr { get; set; }

        [StringLength(150, ErrorMessage = "Website cannot have more than 150 characters!", MinimumLength = 1)]
        [Display(Prompt = "Paste your link here!")]
        public String Website { get; set; }

        [Required(ErrorMessage = "Role Required!")]
        public int RoleID { get; set; }
    }
}
