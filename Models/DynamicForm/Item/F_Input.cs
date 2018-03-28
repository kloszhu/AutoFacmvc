using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DynamicForm.Item
{
    public enum InputTypeEnum {
        
    }
    public class F_Input:F_FormItem
    {
        public InputTypeEnum? MyProperty { get; set; }
    }
}
