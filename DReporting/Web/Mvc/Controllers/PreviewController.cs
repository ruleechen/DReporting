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
        public ActionResult Index(string templateId, string dataProviderId)
        {
            return View("Index", VM(templateId, dataProviderId));
        }

        public ActionResult Callback(string templateId, string dataProviderId)
        {
            return PartialView("Viewer", VM(templateId, dataProviderId));
        }

        private ViewerVM VM(string templateId, string dataProviderId)
        {
            var template = this.ReportStorage.GetTemplate(templateId);

            return new ViewerVM
            {
                TemplateID = templateId,
                TemplateName = template.TemplateName,
                DataProviderId = dataProviderId,
                XtraReport = template.XtraReport
            };
        }

        public ActionResult Export(string templateId)
        {
            var template = this.ReportStorage.GetTemplate(templateId);
            return DocumentViewerExtension.ExportTo(template.XtraReport);
        }
    }
}