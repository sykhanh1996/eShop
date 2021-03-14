using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eShop.BackendServer.Authorization;
using eShop.BackendServer.Constants;
using eShop.BackendServer.Data;
using eShop.BackendServer.Data.Entities;
using eShop.BackendServer.Helpers;
using eShop.BackendServer.Models.ViewModels.Contents;
using eShop.BackendServer.Models.ViewModels.Systems;
using eShop.BackendServer.Services.Interfaces;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace eShop.BackendServer.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductsController> _logger;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ProductsController> _localizer;
        private readonly IString _returnString;

        public ProductsController(ApplicationDbContext context,
            ILogger<ProductsController> logger,
            IMapper mapper,
            IStringLocalizer<ProductsController> localizer,
            IString returnString)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            _localizer = localizer;
            _returnString = returnString;
        }
        //[HttpPost]
        //[ClaimRequirement(FunctionCode.PRODUCT, CommandCode.CREATE)]
        //[ApiValidationFilter]
        //public async Task<IActionResult> PostFunction([FromBody] ProductCreateRequest request)
        //{
        //    _logger.LogInformation(_localizer["BeginProduct"]);
        //    var errMess = _returnString.ReturnString(_localizer["IdExisted"], request.Id);

        //    var dbFunction = await _context.Functions.FindAsync(request.Id);
        //    if (dbFunction != null)
        //        return BadRequest(new ApiBadRequestResponse(errMess));//dung dependecy
        //    if (result > 0)
        //    {
        //        _logger.LogInformation(_localizer["EndPostFunctionSuccess"]);

        //        return CreatedAtAction(nameof(GetById), new { id = function.Id }, request);
        //    }

        //    _logger.LogInformation(_localizer["EndPostFunctionFail"]);

        //    return BadRequest(new ApiBadRequestResponse(_localizer["CreateFunctionFail"]));

        //}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            var productVm = _mapper.Map<ProductVm>(product);
            return Ok(productVm);
        }
    }
}
