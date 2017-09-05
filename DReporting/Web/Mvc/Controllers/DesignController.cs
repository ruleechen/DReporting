using DevExpress.Web.Mvc;
using DevExpress.XtraReports.UI;
using DReporting.Models;
using DReporting.Web.Mvc.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
                template = TemplateMgr.GetDefaultTemplate();
            }
            else
            {
                template = TemplateMgr.GetTemplate(templateId);
            }

            var query = HttpUtility.ParseQueryString(Request.Url.Query);

            var dataSources = DataProviderMgr.QueryDataProviders().ToDictionary(
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
            var xmlContent = ReportDesignerExtension.GetReportXml("reportDesigner");
            var xtraReport = XtraReport.FromStream(new MemoryStream(xmlContent), true);

            var old = TemplateMgr.GetTemplate(templateId);

            var model = TemplateMgr.SaveTemplate(new TemplateModel
            {
                TemplateID = templateId,
                TemplateName = templateName,
                TemplateCode = old != null ? old.TemplateCode : null,
                CategoryID = old != null ? old.CategoryID : null,
                CreationTime = old != null ? old.CreationTime : DateTime.UtcNow,
                LastUpdateTime = old != null ? DateTime.UtcNow : new Nullable<DateTime>(),
                XtraReport = xtraReport
            });

            return Content(Url.Action("Index", "Design", new { Area = ReportingGlobal.AreaName, TemplateID = model.TemplateID, ReturnUrl = returnUrl }));
        }
    }
}
