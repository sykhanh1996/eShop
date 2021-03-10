using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.BackendServer.Services.Interfaces
{
    public interface IString
    {
        string ReturnString(string replaceString, string text);
    }
}
