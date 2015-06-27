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
        public ActionResult Index(string reportId, string dataProviderId)
        {
            var report = this.ReportStorage.GetReport(reportId);

            if (!string.IsNullOrEmpty(dataProviderId))
            {
                var query = HttpUtility.ParseQueryString(Request.Url.Query);
                var provider = this.ReportDataMgr.GetDataProvider(dataProviderId);

                report.XtraReport.DataSourceDemanded += (object sender, EventArgs e) =>
                {
                    ((XtraReport)sender).DataSource = provider.Entity.GetDataSource(query, false);
                };
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