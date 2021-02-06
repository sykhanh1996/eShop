using eShop.BackendServer.Data.Enums;
using eShop.BackendServer.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eShop.BackendServer.Data.Entities
{
    [Table("Attributes")]
    public class Attributes : ISortable, ISwitchable, IDateTracking
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public int SortOrder { get; set; }
        public string BackendType { get; set; }
        public Status Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string ModifiedBy { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string CreatedBy { get; set; }

        public ICollection<AttibuteValueText> AttibuteValueText { get; set; }
        public ICollection<AttributeOption> AttributeOption { get; set; }
        public ICollection<AttributeValueDateTime> AttributeValueDateTime { get; set; }
        public ICollection<AttributeValueDecimal> AttributeValueDecimal { get; set; }
        public ICollection<AttributeValueInt> AttributeValueInt { get; set; }
        public ICollection<AttributeValueVarchar> AttributeValueVarchar { get; set; }
    }
}