using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using DevExpress.XtraReports.UI;
using DReporting.Models;

namespace DReporting.Web.Area.Controllers
{
    public class PreviewController : ControllerBase
    {
        public ActionResult Index(string reportId, string dataSourceId)
        {
            var report = this.ReportStorage.GetReport(reportId);

            //var dataSource = this.ReportService.GetDataSource(dataSourceId);
            //report.XtraReport.DataSource = dataSource;
            //report.XtraReport.FillDataSource();

            return View("Index", new ViewerVM
            {
                ReportId = reportId,
                ReportName = report.ReportName,
                XtraReport = report.XtraReport
            });
        }

        public ActionResult Callback(string reportId)
        {
            var report = this.ReportStorage.GetReport(reportId);

            return PartialView("Viewer", new ViewerVM
            {
                ReportId = reportId,
                ReportName = report.ReportName,
                XtraReport = report.XtraReport
            });
        }

        public ActionResult Export(string reportId)
        {
            var report = this.ReportStorage.GetReport(reportId);
            return DocumentViewerExtension.ExportTo(report.XtraReport);
        }
    }
}