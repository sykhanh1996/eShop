using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShop.BackendServer.Data.Enums;

namespace eShop.BackendServer.Data.Interfaces
{
    public interface ISwitchable
    {
        Status Status { set; get; }
    }
}
