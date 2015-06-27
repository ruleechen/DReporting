using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DReporting.Models;
using DReporting.Services;

namespace DReporting.Web.Area.Controllers
{
    public class DesignController : ControllerBase
    {
        public ActionResult Index(string reportId)
        {
            return View(new DesignerVM
            {
                Report = this.ReportService.GetReport(reportId)
            });
        }

        public ActionResult Create()
        {
            return View(new DesignerVM
            {
                Report = this.ReportService.DefaultReportTemplate()
            });
        }

        [HttpPost]
        public ActionResult Save(string reportId)
        {
            var xmlContent = DevExpress.Web.Mvc.ReportDesignerExtension.GetReportXml("designer");
            this.ReportService.SaveReport(reportId, xmlContent);
            return new EmptyResult();
        }
    }
}
