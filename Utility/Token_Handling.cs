using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace evoting.Utility
{
    public class Token_Handling
    {
        public static string Get_Token_FromHeader(IHeaderDictionary _customHeads)
        {
            
                if (_customHeads.ContainsKey("Token"))
                {
                    return _customHeads["Token"];
                }
                else
                {
                    throw new CustomException.MissingToken();
                }
            
            
        }
    }
}
