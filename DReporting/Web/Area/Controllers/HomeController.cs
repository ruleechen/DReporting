using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DReporting.Models;

namespace DReporting.Web.Area.Controllers
{
    public class HomeController : ControllerBase
    {
        public ActionResult Index()
        {
            return View(new HomeVM
            {
                Reports = this.ReportStorage.AllReports(),
                DataSources = this.ReportDatas.AllDataSources()
            });
        }

        public ActionResult Delete(string reportId)
        {
            this.ReportStorage.RemoveReport(reportId);
            return RedirectToAction("Index", "Home", new { Area = Consts.AreaName });
        }
    }
}
