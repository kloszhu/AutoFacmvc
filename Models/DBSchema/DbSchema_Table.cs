using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public  class DbSchema_Table
    {
        public string Table_Name { get; set; }
        public string Table_Content { get; set; }
        public IEnumerable<DbSchema_Columns> DbSchema_Columns { get; set; } = new List<DbSchema_Columns>(); 
    }
}
