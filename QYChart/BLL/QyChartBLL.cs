using QYChart.Helper;
using QYChart.Helper.Static;
using QYChart.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QYChart.BLL
{
    public class QyChartBLL
    {
        #region 底层架构
        /// <summary>
        ///1. 获取tocken
        /// </summary>
        /// <returns></returns>
        public static Access_Tocken GetTocken()//有用
        {
            string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={0}&corpsecret={1}", WXConfig.CorpID, WXConfig.Secret);
            try
            {
                var entity = Newtonsoft.Json.JsonConvert.DeserializeObject<Access_Tocken>(HttpMethods.HttpGet(url));
                return entity;
            }
            catch { throw new Exception(); }

        }
        /// <summary>
        /// 4.获取当前登录用户
        /// </summary>
        /// <param name="CODE"></param>
        /// <returns></returns>
        public static AuthUser GetCurrentUser(string CODE, string Access_Tocken)//有用
        {

            string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/getuserinfo?access_token={0}&code={1}", Access_Tocken, CODE);
            try
            {
                var json = HttpMethods.HttpGet(url);
              
                var entity = Newtonsoft.Json.JsonConvert.DeserializeObject<AuthUser>(json);

                return entity;
            }
            catch { throw new Exception(); }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Access_Tocken"></param>
        /// <param name="DEPARTMENT_ID"></param>
        /// <param name="FETCH_CHILD">1/0</param>
        /// <returns></returns>
        public static DeptUserList GetDeptUser(string Access_Tocken,string DEPARTMENT_ID,int FETCH_CHILD) {
            string url=string.Format( "https://qyapi.weixin.qq.com/cgi-bin/user/simplelist?access_token={0}&department_id={1}&fetch_child={2}",Access_Tocken,DEPARTMENT_ID,FETCH_CHILD);
            var json = HttpMethods.HttpGet(url);
            var entity = Newtonsoft.Json.JsonConvert.DeserializeObject<DeptUserList>(json);
            return entity;
        }
        
        #endregion
    }
}
