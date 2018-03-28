using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.System
{
    public class C_GroupCompany:BaseModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string _pid { get; set; }
        public IEnumerable<C_Company> Companys { get; set; }
        public IEnumerable<C_Department> Departments { get; set; }
    }
}
