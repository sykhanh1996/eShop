﻿using eShop.BackendServer.Data.Enums;
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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? ParentId { get; set; }
        [DefaultValue(0)]
        public int SortOrder { get; set; }
        public Status Status { get; set; }
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