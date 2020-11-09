using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace evoting.Domain.Models
{
    
    public class FJC_Notice
    {
        public string issuer_name { get; set; }
        public string type { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
    }
}
