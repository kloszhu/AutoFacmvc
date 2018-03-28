using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DynamicForm
{
    public enum FormTypeEnum {
        单表=0
    }
    public  class F_Form:BaseModel
    {
        public string F_FormName { get; set; }
        public FormTypeEnum? F_FormType { get; set; }
        public IEnumerable<F_FormItem> formItems { get; set; }
    }
}
