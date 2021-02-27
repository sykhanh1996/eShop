using System.Collections.Generic;

namespace eShop.BackendServer.Models.ViewModels.Systems
{
    public class UpdatePermissionRequest
    {
        public List<PermissionVm> Permissions { get; set; } = new List<PermissionVm>();
    }
}
