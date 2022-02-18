using System.Collections.Generic;

namespace OnlineShopWeb.Models
{
    public class SetUserRolesViewModel
    {
        public string UserName { get; set; }
        public List<RoleViewModel> UserRoles { get; set; }

        public List<RoleViewModel> AllRoles { get; set; }
    }
}
