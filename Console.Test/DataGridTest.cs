using Com.Models.EasyUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersistentLayer;
namespace Console.Test
{
    public  class DataGridTest
    {
        public Com.Models.EasyUI.DataGrid GetDataGrid()
        {
          
            #region 生成DataGrid默认
            Com.Models.EasyUI.DataGrid dataGrid = new Com.Models.EasyUI.DataGrid();
            List<GridColumns> columnsitem = new List<GridColumns>();
            List<List<GridColumns>> columns = new List<List<GridColumns>>();
            List<Toolbar> toolbars = new List<Toolbar>();
            dataGrid.Title = "数据表格1";
            dataGrid.DataGrid_Code = Guid.NewGuid();
            dataGrid.bytecode = Guid.NewGuid().ToByteArray();
            dataGrid.DataGridName = "Default";
            dataGrid.DataGridCode = DataGridType.默认表格;
            dataGrid.Fit = true;
            dataGrid.SingleSelect = false;
            dataGrid.Collapsible = true;
            dataGrid.Url = "/EasyuiManager/Config/GetAllTree";
            dataGrid.Method = "get";
            dataGrid.FitColumns = true;
            dataGrid.Pagination = true;
            dataGrid.Columns = columns;
            dataGrid.Toolbar = toolbars;
            columnsitem.Add(new GridColumns() { Field="id",Title="id",Width=80  });
            columnsitem.Add(new GridColumns() { Field = "text", Title = "text", Width = 80 });
            columnsitem.Add(new GridColumns() { Field = "url", Title = "url", Width = 80 });
            columns.Add(columnsitem);
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

        public void Insert() {
            PersistentLayer.PersistentManager.DataGrid.MongoDBX.Extentions.Add(GetDataGrid());
        }
    }
}
