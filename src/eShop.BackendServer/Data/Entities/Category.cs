using eShop.BackendServer.Data.Enums;
using eShop.BackendServer.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eShop.BackendServer.Data.Entities
{
    [Table("Categories")]
    public class Category : ISwitchable, ISortable, IDateTracking
    {
        public Category(int? parentId, int sortOrder, Status status, string nameVn, string seoPageTitleVn, string seoAliasVn, string seoKeywordsVn, string seoDescriptionVn)
        {
            ParentId = parentId;
            SortOrder = sortOrder;
            Status = status;
            NameVn = nameVn;
            SeoPageTitleVn = seoPageTitleVn;
            SeoAliasVn = seoAliasVn;
            SeoKeywordsVn = seoKeywordsVn;
            SeoDescriptionVn = seoDescriptionVn;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public int SortOrder { get; set; }
        public Status Status { get; set; }

        [MaxLength(255)]
        public string NameVn { get; set; }

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
        public string ModifiedBy { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string CreatedBy { get; set; }

        public ICollection<CategoryTranslation> CategoryTranslations { get; set; }
        public ICollection<ProductInCategory> ProductInCategorys { get; set; }
    }
}