using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace eShop.BackendServer.Data.Entities
{
    public class UserRole : IdentityRole
    {
        public ICollection<Permission> Permissions { get; set; }
    }
}