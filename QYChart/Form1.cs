using QYChart.BLL;
using QYChart.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using QYChart.Model.Entity;
using Dapper;

namespace QYChart
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
       
        private void button1_Click(object sender, EventArgs e)
        {
           var entity=  QyChartBLL.GetTocken();
            textBox1.Text = entity.access_token;
          
        }
        private List<W_DeptUser> deptUsers { get; set; } = new List<W_DeptUser>();
        private void button2_Click(object sender, EventArgs e)
        {
            int i = 0;
            if (checkBox1.Checked) {
             
                i = 1;
            }
            else {
              
                i = 0;
            }
        
            var entity=  QyChartBLL.GetDeptUser(textBox1.Text, textBox2.Text,i);

            deptUsers = entity.userlist;

            MessageBox.Show(deptUsers.Count.ToString());
        }
       
        private void button3_Click(object sender, EventArgs e)
        {
          List<W_DeptUser> list=  deptUsers.Select(a => new W_DeptUser() { userid=a.userid, name=a.name }).ToList();
           int i = InsertW_DeptUser(list);
            MessageBox.Show(i > 0 ? "执行成功" : "执行失败");

        }
        private  List<W_LoginUser> GetWLoginUser()
        {
            string sql = @"
SELECT ID ,
       C_UserInfo_code ,
       W_UserID ,
       Name FROM dbo.W_LoginUser ";
          

            return db.Query<W_LoginUser>(sql).ToList();

        }
        private int InsertW_DeptUser(List<W_DeptUser> list) {
            int i = 0;
            if (db.State == ConnectionState.Closed)
            {
                db.Open();
            }
            string sql = @"
INSERT INTO dbo.W_DeptUser
        ( userid, name )
VALUES  ( @userid, @name   )";
           
          i=  db.Execute(sql,list);
            db.Close();
            return i;
        }

        private int tongbu() {
            string sql= @"INSERT INTO dbo.W_LoginUser
        (C_UserInfo_code, W_UserID, Name)
SELECT C_Userinfo_code, userid, C_Userinfo_loginID FROM dbo.W_DeptUser LEFT JOIN dbo.C_Userinfo ON
name = C_Userinfo_loginID AND C_Userinfo_isDelete = 0
WHERE C_Userinfo_code IS NOT  NULL
AND NOT EXISTS(SELECT 1 FROM dbo.W_LoginUser WHERE name = C_Userinfo_loginID)";
           return   db.Execute(sql);
        }

       static  string connectstring = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
        IDbConnection db = new SqlConnection(connectstring);
     

        private void button5_Click(object sender, EventArgs e)
        {
            int t = tongbu();
            MessageBox.Show("同步：" + t.ToString() + " 条数据");
        }

        private void button6_Click(object sender, EventArgs e)
        {
   
            try
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
             db.Execute(DeptUserList.dropTable());
                db.Close();
            }
            catch {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
              db.Execute(DeptUserList.CreateTable());
                db.Close();
            }
            MessageBox.Show("执行成功");
        }
    }
}
