using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using Test.Model;
using Common;
using System.Reflection;

namespace Test.Dal
{
    public class DBSchemaDal
    {
        string constr = "data source=.;user id=sa;password=sa123456;database=ceshihuanjing";
        public IEnumerable<DbSchema_Columns> GetColumn(DbSchema_Columns entity) {
            IDbConnection db = new SqlConnection(constr);
          return  db.Query<DbSchema_Columns>(entity.Search(entity), entity);
        }
        public IEnumerable<DbSchema_Columns> GetAllColumn() {
            return GetColumn(new DbSchema_Columns());
        }
        public IEnumerable<DbSchema_Table> GetAllTable() {
            DbSchema_Columns dbSchema_ = new DbSchema_Columns();
            return dbSchema_.GetTable(GetAllColumn());
        }

        public void CreateObjectTable(object obj,string TableName) {
            IDbConnection db = new SqlConnection(constr);
            if (db.State== ConnectionState.Closed)
            {
                db.Open();

            }
            db.Execute(obj.ObjToSql_Create(TableName));
            db.Close();
        }

        public void Insert<T>(object obj,string TableName) where T:class,new()
        {
         
          
          
            PropertyInfo[] propertys = typeof(T).GetProperties();
            string sql = "insert into " + TableName + " (\n";
            sql += string.Join(",", propertys.Select(a => a.Name).ToArray());
            sql+= ")\n  values (\n";
            sql += string.Join(",", propertys.Select(a => "@"+a.Name).ToArray());
            sql += ")\n";
            IDbConnection db = new SqlConnection(constr);
            if (db.State == ConnectionState.Closed)
            {
                db.Open();

            }
            db.Execute(sql,obj);
            db.Close();
        }
    }
}
