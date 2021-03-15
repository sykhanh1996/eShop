using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using eShop.BackendServer.Authorization;
using eShop.BackendServer.Constants;
using eShop.BackendServer.Data;
using eShop.BackendServer.Data.Entities;
using eShop.BackendServer.Helpers;
using eShop.BackendServer.Models.ViewModels.Contents;
using eShop.BackendServer.Models.ViewModels.Systems;
using eShop.BackendServer.Services;
using eShop.BackendServer.Services.Interfaces;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;

namespace eShop.BackendServer.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductsController> _logger;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ProductsController> _localizer;
        private readonly IString _returnString;
        private readonly ISequenceService _sequenceService;

        public ProductsController(ApplicationDbContext context,
            ILogger<ProductsController> logger,
            IMapper mapper,
            IStringLocalizer<ProductsController> localizer,
            IString returnString,
            ISequenceService sequenceService)
        {
            _context = context;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
            _localizer = localizer;
            _returnString = returnString;
            _sequenceService = sequenceService;
        }
        [HttpPost]
        [ClaimRequirement(FunctionCode.SYSTEM_FUNCTION, CommandCode.CREATE)]
        [ApiValidationFilter]
        public async Task<IActionResult> PostFunction([FromBody] ProductCreateRequest request)
        {
            _logger.LogInformation(_localizer["BeginProduct"]);

            var product = _mapper.Map<Product>(request);
            var productTranslation = _mapper.Map<ProductTranslation>(request);
            if (string.IsNullOrEmpty(request.SeoAlias))
            {
                productTranslation.SeoAlias = TextHelper.ToUnsignString(productTranslation.Name);
            }
            product.Id = await _sequenceService.GetProductNewId();
            productTranslation.ProductId = product.Id;
            productTranslation.LanguageId = CultureInfo.CurrentCulture.Name;

            _context.Products.Add(product);
            _context.ProductTranslations.Add(productTranslation);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                _logger.LogInformation(_localizer["EndPostProductSuccess"]);

                var test = CreatedAtAction(nameof(GetById), new { id = product.Id }, request);
                return test;
            }

            _logger.LogInformation(_localizer["EndPostProductFail"]);

            return BadRequest(new ApiBadRequestResponse(_localizer["CreateProductFail"]));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = from p in _context.Products
                          join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                          where p.Id.Equals(id) && pt.LanguageId.Equals(CultureInfo.CurrentCulture.Name)
                          select new { p, pt };
            var item = await product.Select(pr => new ProductVm()
            {
                Id = pr.p.Id,
                Sku = pr.p.Sku,
                ImageUrl = pr.p.ImageUrl,
                ImageList = pr.p.ImageList,
                ThumbImage = pr.p.ThumbImage,
                ViewCount = pr.p.ViewCount,
                Waranty = pr.p.Waranty,
                Price = pr.p.Price,
                PromotionPrice = pr.p.PromotionPrice,
                OriginalPrice = pr.p.OriginalPrice,
                Status = pr.p.Status,
                Name = pr.pt.Name,
                Description = pr.pt.Description,
                Content = pr.pt.Content,
                SeoPageTitle = pr.pt.SeoPageTitle,
                SeoAlias = pr.pt.SeoAlias,
                SeoKeywords = pr.pt.SeoKeywords,
                SeoDescription = pr.pt.SeoKeywords
            }).ToListAsync();

            if (item == null)
                return NotFound();

            return Ok(item);
        }
    }
}
