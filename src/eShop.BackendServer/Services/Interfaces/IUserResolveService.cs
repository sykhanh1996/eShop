using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShop.BackendServer.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace eShop.BackendServer.Services.Interfaces
{
    public interface IUserResolveService
    {
        string GetUser();
    }
}
