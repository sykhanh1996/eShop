using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eShop.BackendServer.Data;
using eShop.BackendServer.Data.Entities;
using eShop.BackendServer.Helpers;
using eShop.BackendServer.Models.ViewModels.Contents;
using eShop.BackendServer.Services.Interfaces;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace eShop.BackendServer.Controllers
{
    public class BillsController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BillsController> _logger;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<BillsController> _localizer;
        private readonly IString _returnString;

        public BillsController(ApplicationDbContext context,
            ILogger<BillsController> logger,
            IMapper mapper,
            IStringLocalizer<BillsController> localizer,
            IString returnString)
        {
            _context = context;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
            _localizer = localizer;
            _returnString = returnString;
        }
     
    }
}
