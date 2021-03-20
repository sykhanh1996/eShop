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
using eShop.BackendServer.Data.Enums;
using eShop.BackendServer.Helpers;
using eShop.BackendServer.Models.ViewModels;
using eShop.BackendServer.Models.ViewModels.Contents;
using eShop.BackendServer.Models.ViewModels.Systems;
using eShop.BackendServer.Services;
using eShop.BackendServer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace eShop.BackendServer.Controllers
{
    public class CategoriesController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CategoriesController> _logger;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<CategoriesController> _localizer;
        private readonly IString _returnString;

        public CategoriesController(ApplicationDbContext context,
            ILogger<CategoriesController> logger,
            IMapper mapper,
            IStringLocalizer<CategoriesController> localizer,
            IString returnString)
        {
            _context = context;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
            _localizer = localizer;
            _returnString = returnString;
        }
        [HttpPost]
        //[ClaimRequirement(FunctionCode.SYSTEM_FUNCTION, CommandCode.CREATE)]
        [ApiValidationFilter]
        public async Task<IActionResult> PostCategory([FromBody] CategoryCreateRequest request)
        {
            _logger.LogInformation(_localizer["Begin PostCategory API"]);
            var category = _mapper.Map<Category>(request);

            if (string.IsNullOrEmpty(request.SeoAliasVn))
            {
                category.SeoAliasVn = TextHelper.ToUnsignString(category.NameVn);
            }

            _context.Categories.Add(category);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                _logger.LogInformation(_localizer["EndPostCategorySuccess"]);
                return Ok();
            }
            _logger.LogInformation(_localizer["EndPostCategoryFail"]);

            return BadRequest(new ApiBadRequestResponse(_localizer["EndPostCategoryFail"]));
        }
        [HttpGet]
        //[ClaimRequirement(FunctionCode.CONTENT_KNOWLEDGEBASE, CommandCode.VIEW)]
        public async Task<IActionResult> GetCategories()
        {
            var categories = _context.Categories;

            var categoriesVm = _mapper.ProjectTo<CategoryVm>(categories).ToListAsync();

            return Ok(categoriesVm);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound();

            var categoryVm = _mapper.Map<CategoryVm>(category);
            return Ok(categoryVm);
        }
        [HttpGet("filter")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategoriesPaging(string filter, int pageIndex, int pageSize)
        {
            var query = _context.Categories.AsQueryable();
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.NameVn.Contains(filter));
            }
            var totalRecords = await query.CountAsync();
            var items = query.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);

            var lstItem = await _mapper.ProjectTo<CategoryVm>(items).ToListAsync();
            var pagination = new Pagination<CategoryVm>
            {
                Items = lstItem,
                TotalRecords = totalRecords,
                PageSize = pageSize,
                PageIndex = pageIndex
            };
            return Ok(pagination);
        }
        [HttpPut("{id}")]
        //[ClaimRequirement(FunctionCode.SYSTEM_FUNCTION, CommandCode.UPDATE)]
        [ApiValidationFilter]
        public async Task<IActionResult> PutCategory(int id, [FromBody] CategoryCreateRequest request)
        {
            var category = await _context.Products.FindAsync(id);
            if (category == null)
                return NotFound(new ApiNotFoundResponse(_returnString.ReturnString(_localizer["Cannot found Category with ID"], id.ToString())));
            var categoryUpdate = _mapper.Map(request, category);

            _context.Products.Update(categoryUpdate);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return NoContent();
            }
            return BadRequest(new ApiBadRequestResponse(_localizer["Create Category is failed"]));
        }
        [HttpPut("{id}/delete")]
        //[ClaimRequirement(FunctionCode.CONTENT_KNOWLEDGEBASE, CommandCode.DELETE)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound();
            category.Status = Status.InActive;
            _context.Categories.Update(category);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                var categoryVm = _mapper.Map<CategoryVm>(category);
                return Ok(categoryVm);
            }
            return BadRequest();
        }
    }
}
