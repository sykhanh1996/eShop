using eShop.BackendServer.Data.Enums;
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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Sku { get; set; }

        [MaxLength(1000)]
        [Column(TypeName = "varchar(1000)")]
        public string ImageUrl { get; set; }

        public string ImageList { get; set; }

        [MaxLength(1000)]
        [Column(TypeName = "varchar(1000)")]
        public string ThumbImage { get; set; }

        [DefaultValue(0)]
        public int ViewCount { get; set; }

        public int? Waranty { get; set; }

        [DefaultValue(0)]
        public decimal Price { get; set; }

        public decimal? PromotionPrice { get; set; }
        public decimal OriginalPrice { get; set; }
        public Status Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string CreatedBy { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string ModifiedBy { get; set; }

        public ICollection<Attachment> Attachments { get; set; }
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