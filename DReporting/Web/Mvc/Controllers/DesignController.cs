using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using DevExpress.XtraReports.UI;
using DReporting.Models;
using DReporting.Web.Mvc.ViewModels;

namespace DReporting.Web.Mvc.Controllers
{
    public class DesignController : ControllerBase
    {
        public ActionResult Index(string reportId)
        {
            return View(Designer(reportId));
        }

        public ActionResult Create()
        {
            return View(Designer(null));
        }

        private DesignerVM Designer(string reportId)
        {
            ReportModel report = null;

            if (string.IsNullOrEmpty(reportId))
            {
                report = this.ReportStorage.GetDefaultReport();
            }
            else
            {
                report = this.ReportStorage.GetReport(reportId);
            }

            var query = System.Web.HttpUtility.ParseQueryString(Request.Url.Query);

            var dataSources = this.ReportDatas.QueryDataSources().ToDictionary(x => x.DataSourceName, x => x.DataSource.QueryData(query, true));

            return new DesignerVM
            {
                ReportID = report.ReportID,
                ReportName = report.ReportName,
                DesignerModel = ReportDesignerExtension.GetModel(report.XtraReport, dataSources)
            };
        }

        [HttpPost]
        public ActionResult Save(string reportId, string displayName)
        {
            var xmlContent = ReportDesignerExtension.GetReportXml("designer");
            var xtraReport = XtraReport.FromStream(new MemoryStream(xmlContent), true);
            xtraReport.DisplayName = displayName;

            var oldReport = this.ReportStorage.GetReport(reportId);

            var model = this.ReportStorage.SaveReport(new ReportModel
            {
                ReportID = reportId,
                ReportName = displayName,
                ReportCode = oldReport != null ? oldReport.ReportCode : null,
                CategoryID = oldReport != null ? oldReport.CategoryID : null,
                CreationTime = oldReport != null ? oldReport.CreationTime : DateTime.UtcNow,
                LastUpdateTime = oldReport != null ? DateTime.UtcNow : new Nullable<DateTime>(),
                XtraReport = xtraReport
            });

            return Content(Url.Action("Index", "Design", new { Area = Contextual.AreaName, ReportID = model.ReportID }));
        }
    }
}
