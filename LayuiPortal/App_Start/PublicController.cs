using Common;
using MongoDBX.Com.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace LayuiPortal.App_Start
{
    public class PublicController<T>:Controller where T:BaseModel
    {
       public  IMongoDBX<T> mongo { get;  set; }
         
        public PublicController() {
            mongo = new MongoDBX<T>();
        }
        public ActionResult Index() {
            return View();
        }
        public ActionResult Detail() {
            return View();
        }
        public ActionResult Create() {
            return View();
        }
        [HttpPost]
        public virtual JsonResult GetList(Expression<Func<T, bool>> expression) {
            var data = mongo.Extentions.GetList(expression);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public virtual JsonResult GetModel(string _id) {
            var data = mongo.Extentions.GetModel(a => a._id == _id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public virtual JsonResult GetPageList(Expression<Func<T, bool>> expression, Expression<Func<T, object>> Sortexpression,int? pagesize, int? CurrentPage, long count) 
            {
            var data = mongo.Extentions.GetPageList(expression, Sortexpression, pagesize, CurrentPage, ref count);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public virtual JsonResult Update(string _id,T Model) {
            Model.Update();
          var data=   mongo.Extentions.Update(a => a._id == _id, Model);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public virtual JsonResult Create(T Model)
        {
             Model.Create();
             mongo.Extentions.Add(Model);
            return Json(new {msg="OK" }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public virtual JsonResult Save(string _id, T Model)
        {
            if (string.IsNullOrEmpty(_id))
            {
                return Create(Model);
            }
            else {
                return Update(_id, Model);
            }
        }
    }
}