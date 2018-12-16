using System.Collections.Generic;

namespace ESW_Shelter.Models
{
    public class UsersIndexViewModel
    {
        public List<Users> Users;
        public Microsoft.AspNetCore.Mvc.Rendering.SelectList RolesType;
        public string RoleType { get; set; }
        public string SearchString { get; set; }
    }
}
