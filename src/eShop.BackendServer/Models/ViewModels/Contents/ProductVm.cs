using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShop.BackendServer.Data.Enums;

namespace eShop.BackendServer.Models.ViewModels.Contents
{
    public class ProductVm
    {
        public int Id { get; set; }
        public string Sku { get; set; }
        public string ImageUrl { get; set; }
        public string ImageList { get; set; }
        public string ThumbImage { get; set; }
        public int ViewCount { get; set; }
        public int? Waranty { get; set; }
        public decimal Price { get; set; }
        public decimal? PromotionPrice { get; set; }
        public decimal OriginalPrice { get; set; }
        public Status Status { get; set; }
    }
}
