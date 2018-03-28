using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDBX.Com.Core;
using Models.System;
using PagedList.Mvc;
using PagedList;
using System.Linq.Expressions;
using Common;
namespace WebApp.Controllers
{
    public class UserinfoController : Controller
    {
        public UserinfoController() {
            mongo = new MongoDBX<C_UserInfo>();
        }
        private IMongoDBX<C_UserInfo> mongo { get; set; }
        // GET: Userinfo
        public ActionResult Index(C_UserInfo info, int? page)
        {
            var expression= Common.LinqExtend.True<C_UserInfo>();

            if (!string.IsNullOrEmpty(info.LoginName))
            {
                expression.AndAlso<C_UserInfo>(a => a.LoginName == info.LoginName);
            }
            expression.AndAlso(a => a.IsDelete == ModelDelete.未删除);
            long count = 0;
            IPagedList<C_UserInfo> list =  mongo.Extentions.GetPageList(expression, a=>a.CreateTime,20,page,ref count).ToPagedList(!page.HasValue?1:page.Value,20);
            return View(list);
        }

        // GET: Userinfo/Details/5
        public ActionResult Details(string id)
        {
            var entity = mongo.Extentions.GetModel(a => a._id == id);
            return View(entity);
        }

        // GET: Userinfo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Userinfo/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection,C_UserInfo model)
        {
            try
            {
                // TODO: Add insert logic here
                TryUpdateModel<C_UserInfo>(model);
                 model.Create();
                 mongo.Extentions.Add(model);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Userinfo/Edit/5
        public ActionResult Edit(string id)
        {
            var entity = mongo.Extentions.GetModel(a => a._id == id);
            return View(entity);
        }

        // POST: Userinfo/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection,C_UserInfo model)
        {
            try
            {
                // TODO: Add update logic here
                TryUpdateModel<C_UserInfo>(model);
                mongo.Extentions.Update(a=>a._id==id,model);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Userinfo/Delete/5
        public ActionResult Delete(string id)
        {
            var entity = mongo.Extentions.GetModel(a => a._id == id);
            return View(entity);
        }

        // POST: Userinfo/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection, C_UserInfo model)
        {
            try
            {
                // TODO: Add delete logic here
                TryUpdateModel<C_UserInfo>(model);
                mongo.Extentions.Delete(a => a._id == id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
