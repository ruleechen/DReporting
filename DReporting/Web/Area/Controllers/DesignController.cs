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

namespace DReporting.Web.Area.Controllers
{
    public class DesignController : ControllerBase
    {
        public ActionResult Index(string reportId)
        {
            var report = this.ReportStorage.GetReport(reportId);
            var dataSources = this.ReportDatas.QueryDataSources().ToDictionary(x => x.DataSourceId, x => (object)x.DataSource);

            return View(new DesignerVM
            {
                ReportId = report.ReportId,
                ReportName = report.ReportName,
                DesignerModel = ReportDesignerExtension.GetModel(report.XtraReport, dataSources)
            });
        }

        public ActionResult Create()
        {
            var report = this.ReportStorage.GetDefaultReport();
            var dataSources = this.ReportDatas.QueryDataSources().ToDictionary(x => x.DataSourceId, x => (object)x.DataSource);

            return View(new DesignerVM
            {
                ReportId = null,
                ReportName = report.ReportName,
                DesignerModel = ReportDesignerExtension.GetModel(report.XtraReport, dataSources)
            });
        }

        [HttpPost]
        public ActionResult Save(string reportId, string displayName)
        {
            if (string.IsNullOrEmpty(reportId))
            {
                reportId = Guid.NewGuid().ToString();
            }

            var xmlContent = ReportDesignerExtension.GetReportXml("designer");
            var report = DevExpress.XtraReports.UI.XtraReport.FromStream(new MemoryStream(xmlContent), true);
            report.DisplayName = displayName;

            var reportStream = new MemoryStream();
            report.SaveLayout(reportStream);
            this.ReportStorage.SaveReport(reportId, reportStream.GetBuffer());

            return Content(Url.Action("Index", "Design", new { Area = Consts.AreaName, ReportId = reportId }));
        }
    }
}
