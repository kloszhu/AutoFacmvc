using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QYChart.Model
{
    public class DeptUserList
    {
        //        {
        //   "errcode": 0,
        //   "errmsg": "ok",
        //   "userlist": [
        //           {
        //                  "userid": "zhangsan",
        //                  "name": "李四",
        //                  "department": [1, 2]
        //    }
        //     ]
        //}
        public int errcode { get; set; }
        public string errmsg { get; set; }
        public List<W_DeptUser> userlist { get; set; } = new List<W_DeptUser>();

        public static string CreateTable() {
            string sql = @"create table W_DeptUser
(
id int identity(1,1) primary key,
userid varchar(50) null,
name varchar(50) null,
wxdept varchar(50) null
)";
            return sql;
        }
        public static string dropTable() {
            string sql = "truncate table W_DeptUser";
            return sql;
        }
    }
    public class W_DeptUser {
        public string userid { get; set; }
        public string name { get; set; }
        public int[] department { get; set; }
      
    }
}
