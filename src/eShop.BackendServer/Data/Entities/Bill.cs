using eShop.BackendServer.Data.Enums;
using eShop.BackendServer.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eShop.BackendServer.Data.Entities
{
    [Table("Bills")]
    public class Bill : ISortable, ISwitchable, IHasSeoMetaData, IDateTracking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string UserId { get; set; }
        public int? ParentId { get; set; }
        public int SortOrder { get; set; }
        public Status Status { get; set; }

        [MaxLength(255)]
        public string SeoPageTitle { get; set; }

        [MaxLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string SeoAlias { get; set; }

        [MaxLength(255)]
        public string SeoKeywords { get; set; }

        [MaxLength(255)]
        public string SeoDescription { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string ModifiedBy { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string CreatedBy { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { set; get; }

        public ICollection<Attachment> Attachments { get; set; }
        public ICollection<BillDetail> BillDetails { get; set; }
    }
}