using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using DevExpress.XtraReports.UI;
using DReporting.Models;
using DReporting.Services;

using DevExpress.Web;
using DevExpress.Web.Mvc.UI;

namespace DReporting.Web.Area.Controllers
{
    public class DesignController : ControllerBase
    {
        public ActionResult Index(string reportId)
        {
            var template = this.ReportService.GetReport(reportId);
            var dataSources = this.ReportService.AllDataSources().ToDictionary(x => x.Value.DataSourceName, x => (object)x.Value);

            return View(new DesignerVM
            {
                ReportId = reportId,
                DesignerModel = ReportDesignerExtension.GetModel((XtraReport)template, dataSources)
            });
        }

        public ActionResult Create()
        {
            var template = this.ReportService.DefaultReportTemplate();
            var dataSources = this.ReportService.AllDataSources().ToDictionary(x => x.Value.DataSourceName, x => (object)x.Value);

            return View(new DesignerVM
            {
                ReportId = null,
                DesignerModel = ReportDesignerExtension.GetModel((XtraReport)template, dataSources)
            });

        //    new HtmlHelper().DevExpress().ReportDesigner(x=>{
        //        x.ClientSideEvents.SaveCommandExecute
        //});
        }

        [HttpPost]
        public ActionResult Save(string reportId)
        {
            if (string.IsNullOrEmpty(reportId))
            {
                reportId = Guid.NewGuid().ToString();
            }

            var xmlContent = DevExpress.Web.Mvc.ReportDesignerExtension.GetReportXml("designer");

            this.ReportService.SaveReport(reportId, xmlContent);

            return Content(Url.Action("Index", "Design", new { Area = "Reporting", ReportId = reportId }));
        }
    }
}
