﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eShop.BackendServer.Data.Entities
{
    [Table("Functions")]
    public class Function
    {
        public Function()
        {

        }
        public Function(string id, string url, int sortOrder, string parentId, string icon, string nameTemp)
        {
            Id = id;
            Url = url;
            SortOrder = sortOrder;
            ParentId = parentId;
            Icon = icon;
            NameTemp = nameTemp;
        }
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        [Key]
        public string Id { get; set; }

        [MaxLength(255)]
        [Required]
        public string Url { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string ParentId { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Icon { get; set; }
        [MaxLength(255)]
        public string NameTemp { get; set; }
        public ICollection<CommandInFunction> CommandInFunctions { get; set; }
        public ICollection<Permission> Permissions { get; set; }
        public ICollection<FunctionTranslation> FunctionTranslations { get; set; }
    }
}