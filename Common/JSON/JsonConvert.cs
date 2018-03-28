using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Common
{
    public static class JsonNullNotOut
    {
        /// <summary>
        /// 没有值就不解析为JSon
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string NullNotOutJson(this object obj) {
          return   JsonConvert.SerializeObject(obj, new JsonSerializerSettings() { NullValueHandling=NullValueHandling.Ignore  });
        }
        /// <summary>
        /// 将json对象保存成sql语句
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static string JsonSql_Create(this string json) {
            return null;
        }
    }
}
