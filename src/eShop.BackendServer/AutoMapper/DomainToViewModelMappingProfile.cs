using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eShop.BackendServer.Data.Entities;
using eShop.ViewModels.Systems;

namespace eShop.BackendServer.AutoMapper
{
    public class DomainToViewModelMappingProfile: Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<UserRole, RoleVm>().MaxDepth(2);
        }
    }
}
