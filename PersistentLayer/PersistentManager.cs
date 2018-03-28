using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Models.EasyUI;
using Models;
namespace PersistentLayer
{
    public static class PersistentManager
    {
        public static IPersistentModel<List<string>, DataGrid> DataGrid { get; set; } = new PersistentModel<List<string>, DataGrid>(new List<string>(){"DataGrid"});
        public static IPersistentModel<List<string>, EasyUITree> EasyUITree { get; set; } = new PersistentModel<List<string>, EasyUITree>(new List<string>() { "EasyuiTree" });
    }
}
