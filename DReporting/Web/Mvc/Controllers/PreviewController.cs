using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using DevExpress.XtraReports.UI;
using DReporting.Web.Mvc.ViewModels;

namespace DReporting.Web.Mvc.Controllers
{
    public class PreviewController : ControllerBase
    {
        public ActionResult Index(string reportId, string dataSourceId)
        {
            var report = this.ReportStorage.GetReport(reportId);

            if (!string.IsNullOrEmpty(dataSourceId))
            {
                var query = System.Web.HttpUtility.ParseQueryString(Request.Url.Query);
                var dataSource = this.ReportDataMgr.GetDataSource(dataSourceId).InnerDataSource;
                report.XtraReport.DataSource = dataSource.GetBindingSource(query, false);
                report.XtraReport.FillDataSource();
            }

            return View("Index", new ViewerVM
            {
                ReportID = reportId,
                ReportName = report.ReportName,
                XtraReport = report.XtraReport
            });
        }

        public ActionResult Callback(string reportId)
        {
            var report = this.ReportStorage.GetReport(reportId);

            return PartialView("Viewer", new ViewerVM
            {
                ReportID = reportId,
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