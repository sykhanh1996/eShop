using eShop.BackendServer.Data.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eShop.BackendServer.Data.Entities
{
    [Table("ProductTranslations")]
    public class ProductTranslation : IHasSeoMetaData, IDateTracking
    {
        public ProductTranslation(){}
        public ProductTranslation(int id,string languageId,string name,string description, string content, string seoPageTitle, string seoAlias, string seoKeywords, string seoDescription)
        {
            ProductId = id;
            LanguageId = languageId;
            Name = name;
            Description = description;
            Content = content;
            SeoPageTitle = seoPageTitle;
            SeoAlias = seoAlias;
            SeoKeywords = seoKeywords;
            SeoDescription = seoDescription;
        }
        public int ProductId { get; set; }
        public string LanguageId { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public string Content { get; set; }

        [MaxLength(255)]
        public string SeoPageTitle { get; set; }

        [MaxLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string SeoAlias { get; set; }

        [MaxLength(255)]
        public string SeoKeywords { get; set; }

        [MaxLength(255)]
        public string SeoDescription { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string CreatedBy { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string ModifiedBy { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { set; get; }

        [ForeignKey("LanguageId")]
        public virtual Language Language { set; get; }
    }
}