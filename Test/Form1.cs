using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Test.Model;
using System.Reflection;
using Newtonsoft.Json.Linq;
using System.Collections.Concurrent;
using System.Data.SqlClient;
using Dapper;
using Test.Dal;
using Common;
using System.IO;
using Models;
using Com.Models.EasyUI;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Com.Models.EasyUI.Student student = new Com.Models.EasyUI.Student();
            student.ID = 123;
            string json = student.NullNotOutJson();
            richTextBox1.Text = json;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Com.Models.EasyUI.Student student = new Com.Models.EasyUI.Student();
            //  var data= student.GetType().GetProperties().Select(a=>new {Name=a.Name,Value=a.GetValue(a.Name),Type=a.GetType() }).ToList();
            foreach (var item in student.GetType().GetProperties())
            {
                richTextBox1.Text += item.Name;
                richTextBox1.Text += ":";
                richTextBox1.Text += item.GetValue(student);
                richTextBox1.Text += "\n";
            }
            //comboBox1.DataSource = data;
            //comboBox1.DisplayMember = "Name";
            //comboBox1.ValueMember = "Value";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BaseDTO InsertStudent = new BaseDTO();
            Com.Models.EasyUI.Student student = new Com.Models.EasyUI.Student();
            student.Name = "周杰伦";
            string json = JsonConvert.SerializeObject(student);
            InsertStudent.BaseDTO_Name = "学生插入";
            InsertStudent.BaseDTO_DalSQL = "insert into ATests (Name) values (@Name)";
            InsertStudent.BaseDTO_GenSQL = "insert into ATests (Name) values (@Name)";
            InsertStudent.BaseDTO_InPut = JObject.Parse(json).ToObject<ConcurrentDictionary<string, object>>();
            BaseDTOExeute(ref InsertStudent);
            richTextBox1.Text += InsertStudent.BaseDTO_OutPut.ToString();
            ///测试查询
            BaseDTO searchStudent = new BaseDTO();
            searchStudent.BaseDTO_Name = "学习查询";
            InsertStudent.BaseDTO_DalSQL = "select * from ATests where 1=1 ";
            var paramters = new ConcurrentDictionary<string, object>();


            paramters.AddOrUpdate("Name", searchStudent.BaseDTO_InPut.Where(a => a.Key == "Name").FirstOrDefault().Value,
           (key, oldValue) => oldValue
            );
            InsertStudent.BaseDTO_Parameters = paramters;
            InsertStudent.BaseDTO_InPut = JObject.Parse(json).ToObject<ConcurrentDictionary<string, object>>();
            BaseDTOQuery(ref InsertStudent);
            richTextBox1.Text += Newtonsoft.Json.JsonConvert.SerializeObject(InsertStudent.BaseDTO_OutPut);

        }

        string constr = "data source=.;user id=sa;password=sa123456;database=test";
        string sql = "insert into ATests (Name) values (@Name)";
        private void DapperInsert(ConcurrentDictionary<string, object> keyValues)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            foreach (var item in keyValues)
            {
                dynamicParameters.Add(item.Key, item.Value);
            }

            IDbConnection db = new SqlConnection(constr);
            db.Open();
            db.Execute(sql, dynamicParameters);
            db.Close();
        }

        private void BaseDTOExeute(ref BaseDTO baseDTO)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            ///缺少验证和对应关系
            foreach (var item in baseDTO.BaseDTO_InPut)
            {
                dynamicParameters.Add(item.Key, item.Value);
            }

            IDbConnection db = new SqlConnection(constr);
            db.Open();
            baseDTO.BaseDTO_OutPut = db.Execute(baseDTO.BaseDTO_GenSQL, dynamicParameters);
            db.Close();
        }


        /// <summary>
        /// 单表
        /// </summary>
        /// <param name="baseDTO"></param>
        private void BaseDTOQuery(ref BaseDTO baseDTO)
        {
            if (baseDTO.BaseDTO_Parameters.Count > 0)
            {
                foreach (var item in baseDTO.BaseDTO_Parameters)
                {
                    baseDTO.BaseDTO_GenSQL = baseDTO.BaseDTO_DalSQL;
                    baseDTO.BaseDTO_GenSQL += string.Format("and (  {0}=@{1} ) ", item.Key, item.Key);
                }
            }
            DynamicParameters dynamicParameters = new DynamicParameters();
            ///缺少验证和对应关系
            foreach (var item in baseDTO.BaseDTO_InPut)
            {
                dynamicParameters.Add(item.Key, item.Value);
            }
            IDbConnection db = new SqlConnection(constr);
            db.Open();
            baseDTO.BaseDTO_OutPut = db.Query(baseDTO.BaseDTO_GenSQL, dynamicParameters);
            db.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DBSchemaDal dal = new DBSchemaDal();
            var list = dal.GetAllColumn();
            MessageBox.Show(list.Count().ToString());
            var listtable = dal.GetAllTable();
            MessageBox.Show(listtable.Count().ToString());
            dal.CreateObjectTable(new Models.DbSchema_Columns(), "Schema_Columns");
            dal.CreateObjectTable(new Models.DbSchema_Table(), "Schema_Tables");
            dal.CreateObjectTable(new Com.Models.EasyUI.DataGrid(), "Easy_DataGrid");
            dal.Insert<Models.DbSchema_Columns>(list, "Schema_Columns");
            dal.Insert<Models.DbSchema_Table>(listtable, "Schema_Tables");
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void Json解析为对象_Click(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "tree1.json";
            StreamReader streamReader = new StreamReader(path);
            string json = streamReader.ReadToEnd();
            streamReader.Close();

            JToken jToken = JToken.Parse(json);
            foreach (var item in jToken.Values())
            {

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var trees = GetTree();
            richTextBox1.Text = trees.NullNotOutJson();
            //DiGui(trees, trees.Where(a => a.id == 0).FirstOrDefault());
            richTextBox1.Text += trees.NullNotOutJson();
        }

        //private void DiGui(List<EasyUITree> all, EasyUITree root)
        //{
        //    List<EasyUITree> list = new List<EasyUITree>();
        //    var Currentroot = root;
        //    var childList = all.Where(a => a.pid == root.id);
        //    root.children.AddRange(childList);
        //    if (childList.Count() > 0)
        //    {
        //        foreach (var item in childList)
        //        {
        //            DiGui(all, item);
        //        }
        //    }

        //}

        private List<EasyUITree> GetTree()
        {
            List<EasyUITree> trees = new List<EasyUITree>();
            for (int i = 0; i < 100; i++)
            {
                if (i % 3 == 0)
                {
                    EasyUITree item1 = new EasyUITree();
                    item1.treeid = i;
                    item1.text = "根节点" + i.ToString();
                    item1.pid = null;
                    trees.Add(item1);
                    continue;
                }

                else if (i % 3 == 1)
                {
                    EasyUITree item1 = new EasyUITree();
                    item1.treeid = i;
                    item1.text = "一级节点" + i.ToString();
                    item1.pid = 0;
                    trees.Add(item1);
                    continue;
                }
                else
                {
                    EasyUITree item = new EasyUITree();
                    item.treeid = i;
                    item.text = "金城武" + i.ToString();
                    Random random = new Random();
                    item.pid = random.Next(1, 3);
                    trees.Add(item);
                }
            }
            return trees;
        }

        private void Json转Datatable_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

            PersistentLayer.PersistentManager.DataGrid.MongoDBX.Extentions.Add(DataGrid());


        }

        private Com.Models.EasyUI.DataGrid DataGrid()
        {
            #region 生成DataGrid默认
            Com.Models.EasyUI.DataGrid dataGrid = new Com.Models.EasyUI.DataGrid();
            List<GridColumns> columns = new List<GridColumns>();
            List<List<GridColumns>> listcolumn = new List<List<GridColumns>>();
            List<Toolbar> toolbars = new List<Toolbar>();
            dataGrid.Title = "数据表格1";
            dataGrid.DataGridName = "Default";
            dataGrid.DataGridCode = DataGridType.默认表格;
            dataGrid.Fit = true;
            dataGrid.SingleSelect = false;
            dataGrid.Collapsible = true;
            dataGrid.Url = "json/datagrid_data1.json";
            dataGrid.Method = "get";
            dataGrid.FitColumns = true;
            dataGrid.Pagination = true;
            dataGrid.Columns = listcolumn;
            dataGrid.Toolbar = toolbars;
            for (int i = 0; i < 3; i++)
            {
                var col = new GridColumns();
                col.Field = "id" + i;
                col.Title = "主键" + i;
                col.Width = 80;
                columns.Add(col);
            }
            listcolumn.Add(columns);
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
            return dataGrid;
        }

        //private void Json遍历(string json)
        //{
        //    try
        //    {
        //        var obj = JObject.Parse(json);
        //        foreach (var item in obj)
        //        {
        //            try
        //            {

        //            }
        //            catch
        //            {

        //                throw;
        //            }

        //            richTextBox1.Text += item.Key;
        //            richTextBox1.Text += item.Value;
        //            richTextBox1.Text += "\n";
        //        }
        //    }
        //    catch
        //    {

        //        var list = JArray.Parse(json);
        //        foreach (var item in list)
        //        {
        //            JObject jobj = item as JObject;
        //            foreach (var jitem in jobj)
        //            {
        //                richTextBox1.Text += "  ";
        //                richTextBox1.Text += jitem.Key;
        //                richTextBox1.Text += jitem.Value;
        //                richTextBox1.Text += "\n";
        //            }
        //        }
        //    }
        //}
    }
}
