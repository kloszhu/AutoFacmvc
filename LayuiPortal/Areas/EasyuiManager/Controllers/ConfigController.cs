using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersistentLayer;
using Common;
using Com.Models.EasyUI;

namespace LayuiPortal.Areas.EasyuiManager.Controllers
{
    public class ConfigController : Controller
    {
        // GET: EasyuiManager/Configration
        public ActionResult Index()
        {
            return View();
        }
        [Route("/EasyuiManager/Config/DataGrid")]
        [HttpGet]
        public JsonResult DataGrid(Com.Models.EasyUI.DataGrid data) {
          var datagrid=  PersistentManager.DataGrid.MongoDBX.Extentions.GetModel(a => a.DataGridID==0);

            return Json(datagrid.NullNotOutJson(), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetAllTree(Com.Models.EasyUI.EasyUITree data)
        {
            //   PersistentManager.EasyUITree.MongoDBX.Extentions.Add(DefaultTreeMenu());
            List<Com.Models.EasyUI.EasyUITree> list = new List<EasyUITree>();
            var db = PersistentManager.EasyUITree.MongoDBX.Extentions.GetList(a => a.treeid == 1).ToList();
            list.AddRange(db);
            foreach (var item in db)
            {
                if (item.children.Count()>0)
                {
                    list.AddRange(item.children);
                }
               
            }
            var json = new
            {
                total = list.Count,
                rows = list.Select(a=>new { id=a._id,text=a.text,url=a.url }).ToList()    
            };
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult MenuTree(Com.Models.EasyUI.EasyUITree data)
        {
            // PersistentManager.EasyUITree.MongoDBX.Extentions.Add(DefaultTreeMenu());
            var db = PersistentManager.EasyUITree.MongoDBX.Extentions.GetList(a => a.treeid == 1).ToList();
            return Content(db.NullNotOutJson());
        }
        private Com.Models.EasyUI.EasyUITree DefaultTreeMenu() {
            Com.Models.EasyUI.EasyUITree root = new Com.Models.EasyUI.EasyUITree();
            List<EasyUITree> list = new List<EasyUITree>();
            root.treeid = 1;
            root.text = "EasyUI配置";
            root.children = list;
            var tree11 = new EasyUITree() { treeid = 3, url = "/EasyuiManager/Config/Index", text = "EasyuiTree数据" };
            var tree1= new EasyUITree() { treeid = 2,  text = "EasyuiTree数据" ,children =new List<EasyUITree>() { tree11} };

            list.Add(tree1);
            return root;
        }
    }
}