using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShop.BackendServer.Data.Interfaces;

namespace eShop.BackendServer.Models.ViewModels.Contents
{
    public class AttachmentVm:IDateTracking
    {
        public int Id { get; set; }
        public string FileName { get; set; }

        public string FilePath { get; set; }

        public string FileType { get; set; }

        public long FileSize { get; set; }

        public int ProductId { get; set; }
        public string ModifiedBy { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
