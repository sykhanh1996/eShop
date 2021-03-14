using eShop.BackendServer.Data.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eShop.BackendServer.Data.Entities
{
    [Table("AttributeOptions")]
    public class AttributeOption : ISortable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public int AttributeId { get; set; }

        public int SortOrder { get; set; }

        [ForeignKey("AttributeId")]
        public virtual Attributes Attribute { set; get; }

        public ICollection<AttributeOptionValue> AttributeOptionValue { get; set; }
    }
}