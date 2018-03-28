using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Newtonsoft.Json;
namespace Com.Models.EasyUI
{
    public class EasyUITree:BaseModel
    {
        [JsonProperty("id")]
        /// <summary>
        /// 表格id
        /// </summary>
        public int treeid { get; set; }
        [JsonProperty("text")]
        /// <summary>
        /// 显示文本
        /// </summary>
        public string text { get; set; }
        [JsonProperty("url")]
        /// <summary>
        /// url
        /// </summary>
        public string url { get; set; }
        [JsonProperty("state")]
        /// <summary>
        /// closed/iconCls
        /// </summary>
        public string state { get; set; }
        [JsonProperty("iconCls")]
        /// <summary>
        /// 图标
        /// </summary>
        public string iconCls { get; set; }
        [JsonProperty("checked")]
        /// <summary>
        /// 
        /// </summary>
        public bool?  Checked { get; set; }
        [JsonProperty("pid")]
        [JsonIgnore]
        /// <summary>
        /// PID
        /// </summary>
        public int?  pid { get; set; }
        [JsonProperty("children")]
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<EasyUITree> children { get; set; }

    }

}