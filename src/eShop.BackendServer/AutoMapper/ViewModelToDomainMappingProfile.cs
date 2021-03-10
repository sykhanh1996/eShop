using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eShop.BackendServer.Data.Entities;
using eShop.BackendServer.Models.ViewModels.Systems;

namespace eShop.BackendServer.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<RoleCreateRequest, AppRole>().ConstructUsing(c => new AppRole(c.Id, c.Name));
            CreateMap<UserCreateRequest, User>().ConstructUsing(u => new User(Guid.NewGuid().ToString(),
                u.UserName, u.FirstName, u.LastName, u.Email, u.PhoneNumber, u.Dob));
            CreateMap<FunctionCreateRequest, Function>().ConstructUsing(f => new Function(f.Id,
                f.Url, f.SortOrder, f.ParentId, f.Icon, f.NameTemp));
        }
    }
}
