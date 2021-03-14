using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using eShop.BackendServer.Constants;
using eShop.BackendServer.Data.Entities;
using eShop.BackendServer.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace eShop.BackendServer.Services.Functions
{
    public class UserResolveService : IUserResolveService
    {
        private readonly IHttpContextAccessor _context;
        public UserResolveService(IHttpContextAccessor context)
        {
            _context = context;
        }
        public string GetUser()
        {
            var userClaim = _context.HttpContext.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userClaim != null)
            {
                var user = JsonConvert.DeserializeObject<List<string>>(userClaim.Value);
                return user.FirstOrDefault();
            }
            return null;
        }
    }
}
