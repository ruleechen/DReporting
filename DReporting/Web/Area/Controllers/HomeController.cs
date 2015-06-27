using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DReporting.Web.Area.Controllers
{
    public class HomeController : ControllerBase
    {
        public ActionResult Index()
        {
            var model = this.Service.AllReports().Select(x => x.ReportInfo);
            return View(model);
        }
    }
}
