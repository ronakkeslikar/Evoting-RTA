using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace evoting.Domain.Models
{
    public class FJC_VerifyAccount
    {
        public int doc_id { get; set; }

        public int result { get; set; }

        public string remark_desc { get; set; }
    }
}
