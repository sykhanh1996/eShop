using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eShop.BackendServer.Data.Entities
{
    [Table("AttributeValueInts")]
    public class AttributeValueInt
    {
     
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public int AttributeId { get; set; }
        public int ProductId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string LanguageId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Value { get; set; }

        [ForeignKey("AttributeId")]
        public virtual Attributes Attribute { set; get; }

        [ForeignKey("ProductId")]
        public virtual Product Product { set; get; }

        [ForeignKey("LanguageId")]
        public virtual Language Language { set; get; }
    }
}