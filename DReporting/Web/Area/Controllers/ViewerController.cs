using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using DevExpress.XtraReports.UI;
using DReporting.Models;
using DReporting.Services;

namespace DReporting.Web.Area.Controllers
{
    public class ViewerController : ControllerBase
    {
        public ActionResult Index(string reportId)
        {
            return View("Index", new ViewerVM
            {
                ReportId = reportId,
                Report = this.Service.GetReport(reportId)
            });
        }

        public ActionResult Callback(string reportId)
        {
            return PartialView("Viewer", new ViewerVM
            {
                ReportId = reportId,
                Report = this.Service.GetReport(reportId)
            });
        }

        public ActionResult Export(string reportId)
        {
            var report = this.Service.GetReport(reportId);
            return DocumentViewerExtension.ExportTo((XtraReport)report);
        }
    }
}