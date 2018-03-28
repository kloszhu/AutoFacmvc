using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.Core;
using Com.Models;
using Com.Models.EasyUI;
using Common;

using Newtonsoft.Json;


namespace MongoDBCore
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //
            // Student();
            // DataGrid();
            // GetDataGrid();
            GetFirstGridModel();
        }
        static void GetFirstGridModel() {
            MongoContext mongo = new MongoContext();
           var entity= mongo.GetCollection<DataGridManager>("DataGridManager").FirstOrDefault(a=>a.DataGrid!=null);
           var dics= entity.R_ReflectModel();
            mongo.GetCommonCollection<ReflectEntity>("ReflectEntity").InsertMany(dics); 
            Console.WriteLine("OK");
            Console.ReadKey();

        }
        static void DataGrid()
        {
            #region 生成DataGrid默认
            DataGrid dataGrid = new DataGrid();
            List<GridColumns> columns = new List<GridColumns>();
            List<Toolbar> toolbars = new List<Toolbar>();
            dataGrid.Title = "数据表格";
            dataGrid.DataGridName = "Default";
            dataGrid.DataGridCode = DataGridType.默认表格;
            dataGrid.Fit = true;
            dataGrid.SingleSelect = false;
            dataGrid.Collapsible = true;
            dataGrid.Url = "json/datagrid_data1.json";
            dataGrid.Method = "get";
            dataGrid.FitColumns = true;
            dataGrid.Pagination = true;
            dataGrid.Columns = columns;
            dataGrid.Toolbar= toolbars;
            for (int i = 0; i < 3; i++)
            {
                var col = new GridColumns();
                col.Field = "id"+i;
                col.Title = "主键"+i;
                col.Width = 80;
                columns.Add(col);
            }
            var toolbar1 = new Toolbar() { Text = "刷新", IconCls = "icon-reload", Hander = @"function() {
                 
            //   alert('刷新')
        }" };
            var toolbar2 = new Toolbar() { Text = "添加", IconCls = "icon-add", Hander = @"function() {
                 
            //   alert('添加')
        }" };
            var toolbar3 = new Toolbar() { Text = "修改", IconCls = "icon-edit", Hander = @" function(row, index, item) {
                    console.info(row);
                    console.info(index);
                    console.info(item);
                    alert('修改')
                }" };
            toolbars.Add(toolbar1);
            toolbars.Add(toolbar2);
            toolbars.Add(toolbar3);
            #endregion
            MongoContext mongo = new MongoContext();
            mongo.GetCollection<DataGrid>("DataGrid").InsertOne(dataGrid);
            mongo.GetCollection<DataGridManager>("DataGridManager").InsertOne(new DataGridManager() { DataGrid=dataGrid,Name="默认表格" });
        }
        static void GetDataGrid() {
            MongoContext mongo = new MongoContext();
           var list=   mongo.GetCollection<DataGrid>("DataGrid").FindToList(a => a.Data == null);
             list.WriteAll();
        }
        static void Student()
        {
            MongoContext mongo = new MongoContext();
            mongo.GetCollection<Student>("Student").InsertOne(new Student() { Age = 15 });
            // mongo.GetCollection<Student>("Student").DeleteMany<Student>(a => a.Age == 15);
            List<Student> students = mongo.GetCollection<Student>("Student").FindToList<Student>(a => a.Age == 15);
            List<Books> books = students.First().books as List<Books>;
            students.WriteAll();
            books.WriteAll();
        }
        static void WriteAll(this object obj)
        {
            Console.WriteLine(JsonConvert.SerializeObject(obj, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
            Console.ReadKey();
        }

        static List<Books> Books()
        {
            List<Books> books = new List<Books>();
            for (int i = 0; i < 10; i++)
            {
                var book = new Books() { BookName = "小米" + i.ToString() + "的产生", BookVersion = i.ToString() };
                books.Add(book);
            }
            return books;
        }
    }
}
