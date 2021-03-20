using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShop.BackendServer.Services.Interfaces;

namespace eShop.BackendServer.Services.Functions
{
    public class StringFunction : IString
    {
        public string ReturnString(string replaceString, string text)
        {
            return string.Format(replaceString, text);
        }

        public string ReturnString(string replaceString, string text1, string text2)
        {
            return string.Format(replaceString, text1, text2);
        }
    }
}
