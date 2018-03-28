using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Model
{
   public  class BaseDTO
    {
        //名称
        //记录sql语句
        //记录where条件
        //生成sql语句
        //传入前端数据
        public string BaseDTO_Name { get; set; }
        public string BaseDTO_DalSQL { get; set; }
        public ConcurrentDictionary<string, object> BaseDTO_Parameters { get; set; } = new ConcurrentDictionary<string, object>();
        public string BaseDTO_GenSQL { get; set; }
        public ConcurrentDictionary<string, object> BaseDTO_InPut { get; set; } = new ConcurrentDictionary<string, object>();
        public object BaseDTO_OutPut { get; set; }
    }
}
