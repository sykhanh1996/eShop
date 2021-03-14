using eShop.BackendServer.Data.Enums;
using eShop.BackendServer.Data.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eShop.BackendServer.Data.Entities
{
    [Table("Languages")]
    public class Language : ISortable, ISwitchable
    {
    
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [DefaultValue(false)]
        public bool IsDefault { get; set; }
 
        public int SortOrder { get; set; }
        public Status Status { get; set; }

        public ICollection<AttibuteValueText> AttibuteValueTexts { get; set; }
        public ICollection<AttributeOptionValue> AttributeOptionValues { get; set; }
        public ICollection<AttributeValueDateTime> AttributeValueDateTimes { get; set; }
        public ICollection<AttributeValueDecimal> AttributeValueDecimals { get; set; }
        public ICollection<AttributeValueInt> AttributeValueInts { get; set; }
        public ICollection<AttributeValueVarchar> AttributeValueVarchars { get; set; }
        public ICollection<CategoryTranslation> CategoryTranslations { get; set; }
        public ICollection<FunctionTranslation> FunctionTranslations { get; set; }
        public ICollection<ProductTranslation> ProductTranslations { get; set; }
    }
}