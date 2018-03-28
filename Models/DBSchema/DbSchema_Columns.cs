using System.Collections.Generic;
using System.Data;
using System.Linq;
using Common;

namespace Models
{
    public class DbSchema_Columns
    {
        public string Table_Name { get; set; }
        public string Table_Content { get; set; }
        public int? Column_Order { get; set; }
        public string Column_Name { get; set; }
        public string Column_Content { get; set; }
        public bool? Column_IsIdentity { get; set; }
        public bool? Column_IsPrimaryKey { get; set; }
        public string Column_Type { get; set; }
        public int? Column_Length { get; set; }
        public int? Column_Scale { get; set; }
        public bool? Column_IsNull { get; set; }
        public string Column_Default { get; set; }

        public string Search(DbSchema_Columns columns)
        {
            #region 查询sql
            string sql = @"SELECT * FROM (
SELECT
--表名 = Case When A.colorder=1 Then D.name Else NULL End,
Table_Name=D.name,
Table_Content = Case When A.colorder=1 Then isnull(F.value,NULL) Else NULL End,
Column_Order = A.colorder,
Column_Name = A.name,
Column_Content = isnull(G.[value],NULL),
Column_IsIdentity = Case When COLUMNPROPERTY( A.id,A.name,'IsIdentity')=1 Then 1 ELSE 0 End,
Column_IsPrimaryKey = Case When exists(SELECT 1 FROM sysobjects Where xtype='PK' and parent_obj=A.id and name in (
SELECT name FROM sysindexes WHERE indid in( SELECT indid FROM sysindexkeys WHERE id = A.id AND colid=A.colid))) then 1 else 0 end,
Column_Type = B.name,
--占用字节数 = A.Length,
Column_Length = COLUMNPROPERTY(A.id,A.name,'PRECISION'),
Column_Scale = isnull(COLUMNPROPERTY(A.id,A.name,'Scale'),0),
Column_IsNull = Case When A.isnullable=1 THEN 1  Else 0 End,
Column_Default = isnull(E.Text,NULL),
Column_ID=A.id
FROM
syscolumns A
Left Join
systypes B
On
A.xusertype=B.xusertype
Inner Join
sysobjects D
On
A.id=D.id and D.xtype='U' and D.name<>'dtproperties'
Left Join
syscomments E
on
A.cdefault=E.id
Left Join
sys.extended_properties G
on
A.id=G.major_id and A.colid=G.minor_id
Left Join
 
sys.extended_properties F
On
D.id=F.major_id and F.minor_id=0
) T WHERE 1=1 ";
            if (Table_Name.HasValue()) { sql += " AND Table_Name=@Table_Name "; }
            if (Table_Content.HasValue()) { sql += " AND Table_Content=@Table_Content "; }
            if (Column_Order.HasValue) { sql += " AND Column_Order=@Column_Order "; }
            if (Column_Name.HasValue()) { sql += " AND Column_Name=@Column_Name "; }
            if (Column_Content.HasValue()) { sql += " AND Column_Content=@Column_Content "; }
            if (Column_IsIdentity.HasValue) { sql += " AND Column_IsIdentity=@Column_IsIdentity "; }
            if (Column_IsPrimaryKey.HasValue) { sql += " AND Column_IsPrimaryKey=@Column_IsPrimaryKey "; }
            if (Column_Type.HasValue()) { sql += " AND Column_Type=@Column_Type "; }
            if (Column_Length.HasValue) { sql += " AND Column_Length=@Column_Length "; }
            if (Column_Scale.HasValue) { sql += " AND Column_Scale=@Column_Scale "; }
            if (Column_IsNull.HasValue) { sql += " AND Column_IsNull=@Column_IsNull "; }
            if (Column_Default.HasValue()) { sql += " AND Column_Default=@Column_Default "; }

            sql += " ORDER BY T.Column_ID,T.Column_Order";
            #endregion
            return sql;
        }
        public IEnumerable<DbSchema_Table> GetTable(IEnumerable<DbSchema_Columns> columns) {
          return   columns.GroupBy(a => new { a.Table_Name, a.Table_Content }).Select(a => new DbSchema_Table() { Table_Name= a.Key.Table_Name,Table_Content= a.Key.Table_Content });
        }
    }
}