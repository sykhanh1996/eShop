using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.BackendServer.Data.Interfaces
{
    public interface ISortable
    {
        int SortOrder { set; get; }
    }
}
