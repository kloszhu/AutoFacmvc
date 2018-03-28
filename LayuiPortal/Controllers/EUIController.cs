using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LayuiPortal.Controllers
{
    public class EUIController : Controller
    {
        // GET: EUI
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Data() {
            return View();
        }
    }
}