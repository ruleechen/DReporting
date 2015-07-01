using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using DevExpress.XtraReports.UI;
using DReporting.Models;
using DReporting.Web.Mvc.ViewModels;

namespace DReporting.Web.Mvc.Controllers
{
    public class DesignController : ControllerBase
    {
        public ActionResult Index(string templateId)
        {
            return View(Designer(templateId));
        }

        public ActionResult Create()
        {
            return View(Designer(null));
        }

        private DesignerVM Designer(string templateId)
        {
            TemplateModel template = null;

            if (string.IsNullOrEmpty(templateId))
            {
                template = this.ReportStorage.GetDefaultTemplate();
            }
            else
            {
                template = this.ReportStorage.GetTemplate(templateId);
            }

            var query = HttpUtility.ParseQueryString(Request.Url.Query);

            var dataSources = this.ReportStorage.QueryDataProviders().ToDictionary(
                x => x.Entity.DataProviderName,
                x => x.Entity.GetDataSource(query, true));

            return new DesignerVM
            {
                TemplateID = template.TemplateID,
                TemplateName = template.TemplateName,
                DataSources = dataSources,
                XtraReport = template.XtraReport
            };
        }

        [HttpPost]
        public ActionResult Save(string templateId, string templateName, string returnUrl)
        {
            var xmlContent = ReportDesignerExtension.GetReportXml("designer");
            var xtraReport = XtraReport.FromStream(new MemoryStream(xmlContent), true);

            var old = this.ReportStorage.GetTemplate(templateId);

            var model = this.ReportStorage.SaveTemplate(new TemplateModel
            {
                TemplateID = templateId,
                TemplateName = templateName,
                TemplateCode = old != null ? old.TemplateCode : null,
                CategoryID = old != null ? old.CategoryID : null,
                CreationTime = old != null ? old.CreationTime : DateTime.UtcNow,
                LastUpdateTime = old != null ? DateTime.UtcNow : new Nullable<DateTime>(),
                XtraReport = xtraReport
            });

            return Content(Url.Action("Index", "Design", new { Area = ReportingContext.AreaName, TemplateID = model.TemplateID, ReturnUrl = returnUrl }));
        }
    }
}
