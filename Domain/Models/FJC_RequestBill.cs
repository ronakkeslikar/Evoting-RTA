using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace evoting.Domain.Models
{
    
    public class FJC_RequestBill
    {
        public int event_id { get; set; }
        public string invoice_date { get; set; }
        public string mailed_date { get; set; }
        public string paid_date { get; set; }
    }
}
