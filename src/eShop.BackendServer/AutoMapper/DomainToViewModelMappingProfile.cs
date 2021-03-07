using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eShop.BackendServer.Data.Entities;
using eShop.BackendServer.Models.ViewModels.Systems;

namespace eShop.BackendServer.AutoMapper
{
    public class DomainToViewModelMappingProfile: Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<AppRole, RoleVm>().MaxDepth(2);
            CreateMap<User, UserVm>().MaxDepth(2);
            CreateMap<Function, FunctionVm>().MaxDepth(2);
        }
    }
}
