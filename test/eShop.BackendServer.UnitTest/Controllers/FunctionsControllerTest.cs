using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using eShop.BackendServer.Controllers;
using eShop.BackendServer.Data;
using eShop.BackendServer.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Moq;

namespace eShop.BackendServer.UnitTest.Controllers
{
    public class FunctionsControllerTest
    {
        private ApplicationDbContext _context;
        private Mock<ILogger<FunctionsController>> _mockLogger;
        private readonly IMapper _mapper;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IStringLocalizer<UsersController> _localizer;
    }
}
