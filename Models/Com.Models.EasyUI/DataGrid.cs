using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Models.EasyUI
{
   public  enum DataGridType
    {
        默认表格=0,
        其他=1
    }
    /// <summary>
    /// DataGrid
    /// </summary>
    public class DataGrid:BaseModel
    {
        [JsonIgnore]
        public byte[] bytecode { get; set; }
        [JsonIgnore]
        public Guid? DataGrid_Code { get; set; }
        [JsonIgnore]
        public int DataGridID { get; set; }
        [JsonIgnore]
        public string DataGridName { get; set; }
        [JsonIgnore]
        public DataGridType? DataGridCode { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("pagePosition")]
        public string PagePosition { get; set; }
        [JsonProperty("pageNumber")]
        public int? PageNumber { get; set; }
        [JsonProperty("pageSize")]
        public int? PageSize { get; set; }
        [JsonProperty("pageList")]
        public string pageList { get; set; }
        [JsonProperty("queryParams")]
        public string QueryParams { get; set; }

        [JsonProperty("fit")]
        public bool? Fit { get; set; }

        [JsonProperty("singleSelect")]
        public bool? SingleSelect { get; set; }

        [JsonProperty("ctrlSelect")]
        public bool? CtrlSelect { get; set; }

        [JsonProperty("checkOnSelect")]
        public bool? checkOnSelect { get; set; }
        /// <summary>
        /// If set to true, scroll to the row automatically when selecting it. Available since version 1.5.2.
        /// </summary>
        [JsonProperty("scrollOnSelect")]
        public bool? ScrollOnSelect { get; set; }



        [JsonProperty("selectOnCheck")]
        public bool? SelectOnCheck { get; set; }

        [JsonProperty("collapsible")]
        public bool? Collapsible { get; set; }

        [JsonProperty("nowrap")]
        public bool? NoWrap { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("resizeHandle")]
        public string ResizeHandle { get; set; }
        /// <summary>
        /// Indicate which field is an identity field.
        /// </summary>
        [JsonProperty("idField")]
        public string IdField { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("loadMsg")]
        public string LoadMsg { get; set; }

        [JsonProperty("emptyMsg")]
        public string emptyMsg { get; set; }

        [JsonProperty("resizeEdge")]
        public int? ResizeEdge { get; set; }


        [JsonProperty("fitColumns")]
        public bool? FitColumns { get; set; }

        [JsonProperty("rownumbers")]
        public bool? RowNumbers { get; set; }
        [JsonProperty("autoRowHeight")]
        public bool? AutoRowHeight { get; set; }
        [JsonProperty("striped")]
        public bool? Striped { get; set; }
        [JsonProperty("pagination")]
        public bool? Pagination { get; set; }
        [JsonProperty("columns")]
        public IEnumerable< IEnumerable<GridColumns>> Columns { get; set; }
        [JsonProperty("frozenColumns")]
        public IEnumerable<GridColumns> frozenColumns { get; set; }
        [JsonProperty("toolbar")]
        public IEnumerable<Toolbar> Toolbar { get; set; }
        [JsonProperty("sortName	")]
        public string sortName { get; set; }
        [JsonProperty("sortOrder")]
        public string sortOrder { get; set; }
        [JsonProperty("multiSort")]
        public bool? multiSort { get; set; }
        [JsonProperty("remoteSort")]
        public bool? remoteSort { get; set; }
        [JsonProperty("showHeader")]
        public bool? showHeader { get; set; }
        [JsonProperty("showFooter")]
        public bool? showFooter { get; set; }
        [JsonProperty("scrollbarSize")]
        public int? scrollbarSize { get; set; }
        [JsonProperty("rownumberWidth")]
        public int? rownumberWidth { get; set; }
        [JsonProperty("editorHeight")]
        public int? editorHeight { get; set; }
        [JsonProperty("rowStyler")]
        public string rowStyler { get; set; }
        [JsonProperty("loader")]
        public string loader { get; set; }
        [JsonProperty("loadFilter")]
        public string loadFilter { get; set; }
        [JsonProperty("editors")]
        public string editors { get; set; }
        [JsonProperty("view	")]
        public string view { get; set; }


    }
    /// <summary>
    /// 工具类
    /// </summary>
    public class Toolbar
    {
        [JsonIgnore]
        public int ToolbarID { get; set; }
        [JsonIgnore]
        public int? DataGridID { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("iconCls")]
        public string IconCls { get; set; }
        [JsonProperty("hander")]
        public string Hander { get; set; }
    }
    /// <summary>
    /// 列属性
    /// </summary>
    public class GridColumns
    {
        [JsonIgnore]
        public int GridColumnsID { get; set; }

      

        [JsonProperty("field")]
        public string Field { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("width")]
        public int? Width { get; set; }


        [JsonProperty("align")]
        public string Align { get; set; }

        [JsonProperty("sortable")]
        public bool? Sortable { get; set; }

        [JsonProperty("rowspan")]
        public int? RowSpan { get; set; }

        [JsonProperty("colspan")]
        public int? ColSpan { get; set; }

        [JsonProperty("order")]
        public string Order { get; set; }

        [JsonProperty("resizable")]
        public int? Resizable { get; set; }

        [JsonProperty("fixed")]
        public int? Fixed { get; set; }

        [JsonProperty("hidden")]
        public int? Hidden { get; set; }

        [JsonProperty("checkbox")]
        public bool? Checkbox { get; set; }

        [JsonProperty("formatter")]
        public string Formatter { get; set; }

        [JsonProperty("styler")]
        public string Styler { get; set; }

        [JsonProperty("sorter")]
        public string Sorter { get; set; }

        [JsonProperty("editor")]
        public string Editor { get; set; }



    }


}
