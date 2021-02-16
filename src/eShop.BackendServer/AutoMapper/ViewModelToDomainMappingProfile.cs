using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eShop.BackendServer.Data.Entities;
using eShop.ViewModels.Systems;

namespace eShop.BackendServer.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<RoleCreateRequest, UserRole>().ConstructUsing(c => new UserRole(c.Id,c.Name));
        }
    }
}
