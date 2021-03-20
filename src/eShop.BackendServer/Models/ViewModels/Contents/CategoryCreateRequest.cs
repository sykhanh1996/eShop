using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShop.BackendServer.Data.Enums;

namespace eShop.BackendServer.Models.ViewModels.Contents
{
    public class CategoryCreateRequest
    {
        public int? ParentId { get; set; }
        public int SortOrder { get; set; }
        public Status Status { get; set; }
        public string NameVn { get; set; }
        public string SeoPageTitleVn { get; set; }
        public string SeoAliasVn { get; set; }
        public string SeoKeywordsVn { get; set; }
        public string SeoDescriptionVn { get; set; }
    }
}
