using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.System
{
    public class C_Company:BaseModel
    {
        public string CompanyName { get; set; }
        public string CompanyCode { get; set; }
        public string _pid { get; set; }
    }
}
