using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data;

namespace Common
{
    public static  class CheckNull
    {
        public static bool HasValue(this string value) {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            else {
                return true;
            }
        }

        public static string ObjToSql_Create(this object obj,string dbName) {
            string sql =string.Format( "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[{0}]') AND type in (N'U'))\n",dbName);
            sql += string.Format("create table {0} (\n", dbName);
            var listproperty = obj.GetType().GetProperties();
      
          
            var Items= listproperty.Select(item => new { Item = string.Format("[{0}]  {1} null \n", item.Name, GetTypeByReflection(item.PropertyType).Key) });
            sql += string.Join(",", Items.Select(a=>a.Item).ToArray());
           
            sql += string.Format(")\n");
            return sql;
        }

        public static KeyValuePair<string,DbType>  GetTypeByReflection(Type type) {
        
            if (type == typeof(String))
            {
                return new KeyValuePair<string, DbType>("varchar(200)", DbType.String);
            }
            else if (type == typeof(bool?))
            {
                return new KeyValuePair<string, DbType>("bit", DbType.Boolean);
            }
            else if (type == typeof(bool))
            {
                return new KeyValuePair<string, DbType>("bit", DbType.Boolean);
            }
            else if (type == typeof(int?))
            {
                return new KeyValuePair<string, DbType>("int", DbType.Int32);
            }
            else if (type == typeof(int))
            {
                return new KeyValuePair<string, DbType>("int", DbType.Int32);
            }
            else {
                return new KeyValuePair<string, DbType>("varchar(200)", DbType.String);
            }

        }
    }
}
