using Com.Models.EasyUI;
using LayuiPortal.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace LayuiPortal.Areas.EasyuiManager.Controllers
{
    public class StudentController : PublicController<Student>
    {
        public  JsonResult GetList()
        {

            return base.GetList(a=>a.IsDelete==Common.ModelDelete.未删除);
        }
        public JsonResult Delete(string _id) {
            var entity = base.mongo.Extentions.GetModel(a => a._id == _id);
            entity.IsDelete = Common.ModelDelete.已删除;
           var result=  base.mongo.Extentions.Update(a=>a._id==_id, entity);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}