using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using eShop.BackendServer.Data.Enums;

namespace eShop.BackendServer.Models.ViewModels.Contents
{
    public class ProductCreateRequest
    {
        public int Id { get; set; }
        public string LanguageId{ get; set; }
        public string Sku { get; set; }
        public string ImageUrl { get; set; }
        public string ImageList { get; set; }
        public string ThumbImage { get; set; }
        public int? Waranty { get; set; }
        public decimal Price { get; set; }
        public decimal? PromotionPrice { get; set; }
        public decimal OriginalPrice { get; set; }
        public Status Status { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string SeoPageTitle { get; set; }
        public string SeoAlias { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoDescription { get; set; }
    }
}
