using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace evoting.Authorization
{
    public class Requirement:IAuthorizationRequirement
    {
        public string DomainName { get; }
        public Requirement(string domainName)
        {
            DomainName = domainName;
        }
    }
}
