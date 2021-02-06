using eShop.BackendServer.Data.Enums;
using eShop.BackendServer.Data.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eShop.BackendServer.Data.Entities
{
    [Table("CategoryTranslations")]
    public class CategoryTranslation : ISwitchable, IHasSeoMetaData, IDateTracking
    {
        public int CategoryId { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string LanguageId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string SeoPageTitle { get; set; }

        [MaxLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string SeoAlias { get; set; }

        [MaxLength(255)]
        public string SeoKeywords { get; set; }

        [MaxLength(255)]
        public string SeoDescription { get; set; }

        public Status Status { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string ModifiedBy { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string CreatedBy { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        [ForeignKey("LanguageId")]
        public virtual Language Language { set; get; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { set; get; }
    }
}