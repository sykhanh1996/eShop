using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
using Microsoft.AspNetCore.Http;
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
        private readonly IStorageService _storageService;

        public ProductsController(ApplicationDbContext context,
            ILogger<ProductsController> logger,
            IMapper mapper,
            IStringLocalizer<ProductsController> localizer,
            IString returnString,
            ISequenceService sequenceService,
            IStorageService storageService)
        {
            _context = context;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
            _localizer = localizer;
            _returnString = returnString;
            _sequenceService = sequenceService;
            _storageService = storageService;
        }
        [HttpPost]
        //[ClaimRequirement(FunctionCode.SYSTEM_FUNCTION, CommandCode.CREATE)]
        [ApiValidationFilter]
        public async Task<IActionResult> PostFunction([FromForm] ProductCreateRequest request)
        {
            _logger.LogInformation(_localizer["BeginProduct"]);

            var product = _mapper.Map<Product>(request);

            if (string.IsNullOrEmpty(request.SeoAliasVn))
            {
                product.SeoAliasVn = TextHelper.ToUnsignString(product.NameVn);
            }
            product.Id = await _sequenceService.GetProductNewId();

            //Process attachment
            if (request.Attachments != null && request.Attachments.Count > 0)
            {
                foreach (var attachment in request.Attachments)
                {
                    var attachmentEntity = await SaveFile(product.Id, attachment);
                    _context.Attachments.Add(attachmentEntity);
                }
            }
            _context.Products.Add(product);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                _logger.LogInformation(_localizer["EndPostProductSuccess"]);

                return CreatedAtAction(nameof(GetById), new { id = product.Id }, request);
            }
            _logger.LogInformation(_localizer["EndPostProductFail"]);

            return BadRequest(new ApiBadRequestResponse(_localizer["CreateProductFail"]));
        }
        [HttpGet]
        //[ClaimRequirement(FunctionCode.CONTENT_KNOWLEDGEBASE, CommandCode.VIEW)]
        public async Task<IActionResult> GetProducts()
        {
            var products = _context.Products;

            var productVms = _mapper.ProjectTo<ProductVm>(products).ToListAsync();

            return Ok(productVms);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();
            var productVm = _mapper.Map<ProductVm>(product);
            return Ok(productVm);
        }
        [HttpGet("filter")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductsPaging(string filter, int pageIndex, int pageSize)
        {
            var query = _context.Products.AsQueryable();
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.NameVn.Contains(filter));
            }
            var totalRecords = await query.CountAsync();
            var items = query.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);

            var lstItem = await _mapper.ProjectTo<ProductVm>(items).ToListAsync();
            var pagination = new Pagination<ProductVm>
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
        public async Task<IActionResult> PutProduct(int id, [FromBody] ProductCreateRequest request)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound(new ApiNotFoundResponse(_returnString.ReturnString(_localizer["Cannot found Product with ID"], id.ToString())));
            var productUpdate = _mapper.Map(request, product);
          
            _context.Products.Update(productUpdate);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return NoContent();
            }
            return BadRequest(new ApiBadRequestResponse(_localizer["Create Product is failed"]));
        }
        [HttpPut("{id}/delete")]
        //[ClaimRequirement(FunctionCode.CONTENT_KNOWLEDGEBASE, CommandCode.DELETE)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();
            product.Status = Status.InActive;
            _context.Products.Update(product);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                var productVm = _mapper.Map<ProductVm>(product);
                return Ok(productVm);
            }
            return BadRequest();
        }
        #region Attachment
        [HttpGet("{productId}/attachments")]
        public async Task<IActionResult> GetAttachment(int productId)
        {
            var query = await _context.Attachments
                .Where(x => x.ProductId == productId)
                .Select(c => new AttachmentVm()
                {
                    Id = c.Id,
                    LastModifiedDate = c.LastModifiedDate,
                    CreateDate = c.CreateDate,
                    FileName = c.FileName,
                    FilePath = c.FilePath,
                    FileSize = c.FileSize,
                    FileType = c.FileType,
                    ProductId = c.ProductId
                }).ToListAsync();

            return Ok(query);
        }

        [HttpDelete("{productId}/attachments/{attachmentId}")]
        public async Task<IActionResult> DeleteAttachment(int attachmentId)
        {
            var attachment = await _context.Attachments.FindAsync(attachmentId);
            if (attachment == null)
                return BadRequest(new ApiBadRequestResponse(_returnString.ReturnString(_localizer["Cannot found attachment with ID"], attachmentId.ToString())));

            _context.Attachments.Remove(attachment);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return Ok();
            }
            return BadRequest(new ApiBadRequestResponse(_returnString.ReturnString(_localizer["Delete attachment failed"], attachmentId.ToString())));
        }
        private async Task<Attachment> SaveFile(int productId, IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{originalFileName.Substring(0, originalFileName.LastIndexOf('.'))}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            var attachmentEntity = new Attachment()
            {
                FileName = fileName,
                FilePath = _storageService.GetFileUrl(fileName),
                FileSize = file.Length,
                FileType = Path.GetExtension(fileName),
                ProductId = productId,
            };
            return attachmentEntity;
        }


        #endregion
    }
}
