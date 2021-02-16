using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace eShop.BackendServer.Data.Entities
{
    public class UserRole : IdentityRole
    {
        public UserRole()
        {

        }
        public UserRole(string id, string name)
        {
            Id = id;
            Name = name;
            NormalizedName = name.ToUpper();
        }
        public ICollection<Permission> Permissions { get; set; }
    }
}