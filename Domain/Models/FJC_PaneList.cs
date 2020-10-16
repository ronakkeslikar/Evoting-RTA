using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace evoting.Domain.Models
{
    
    public class FJC_PaneList
    {
        public string event_id { get; set; }
        public string email_id { get; set; }
        public string name { get; set; }
        public int id { get; set; }
    }
}
