using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace evoting.Persistence.Contexts.Sp_SQL_Objects
{
    public class sp_RenameRandomly
    {
        public int id { get; set; }
        public string originalName { get; set; }
        public string newName { get; set; }
    }
}
