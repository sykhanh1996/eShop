﻿using eShop.BackendServer.Data.Enums;
using eShop.BackendServer.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eShop.BackendServer.Data.Entities
{
    [Table("Products")]
    public class Product : ISwitchable, IDateTracking
    {
        public Product() { }
        public Product(string sku, string imageUrl, string imageList, string thumbImage, int? waranty, decimal price,
            decimal? promotionPrice, decimal originalPrice, Status status,string nameVn,string descriptionVn,string contentVn,
            string seoPageTitleVn,string seoAliasVn,string seoKeywordsVn,string seoDescriptionVn)
        {
            Sku = sku;
            ImageUrl = imageUrl;
            ImageList = imageList;
            ThumbImage = thumbImage;
            Waranty = waranty;
            Price = price;
            PromotionPrice = promotionPrice;
            OriginalPrice = originalPrice;
            Status = status;
            NameVn = nameVn;
            DescriptionVn = descriptionVn;
            ContentVn = contentVn;
            SeoPageTitleVn = seoPageTitleVn;
            SeoAliasVn = seoAliasVn;
            SeoKeywordsVn = seoKeywordsVn;
            SeoDescriptionVn = seoDescriptionVn;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Sku { get; set; }

        [MaxLength(1000)]
        [Column(TypeName = "varchar(1000)")]
        public string ImageUrl { get; set; }

        public string ImageList { get; set; }

        [MaxLength(1000)]
        [Column(TypeName = "varchar(1000)")]
        public string ThumbImage { get; set; }


        public int ViewCount { get; set; }

        public int? Waranty { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? PromotionPrice { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal OriginalPrice { get; set; }
        public Status Status { get; set; }
        [MaxLength(255)]
        public string NameVn { get; set; }

        [MaxLength(500)]
        public string DescriptionVn { get; set; }

        public string ContentVn { get; set; }

        [MaxLength(255)]
        public string SeoPageTitleVn { get; set; }

        [MaxLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string SeoAliasVn { get; set; }

        [MaxLength(255)]
        public string SeoKeywordsVn { get; set; }

        [MaxLength(255)]
        public string SeoDescriptionVn { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string CreatedBy { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string ModifiedBy { get; set; }
        public ICollection<AttibuteValueText> AttibuteValueTexts { get; set; }
        public ICollection<AttributeOptionValue> AttributeOptionValues { get; set; }
        public ICollection<AttributeValueDateTime> AttributeValueDateTimes { get; set; }
        public ICollection<AttributeValueDecimal> AttributeValueDecimals { get; set; }
        public ICollection<AttributeValueInt> AttributeValueInts { get; set; }
        public ICollection<AttributeValueVarchar> AttributeValueVarchars { get; set; }
        public ICollection<BillDetail> BillDetails { get; set; }
        public ICollection<ProductInCategory> ProductInCategorys { get; set; }
        public ICollection<ProductTranslation> ProductTranslations { get; set; }
    }
}