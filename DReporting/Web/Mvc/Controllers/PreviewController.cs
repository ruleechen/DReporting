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
            var args = HttpUtility.ParseQueryString(Request.Url.Query);
            args.Remove("TemplateID");
            args.Remove("DataProviderID");
            args.Remove("ReturnUrl");

            var vm = this.VM(templateId, dataProviderId, args.ToString());

            return View("Index", vm);
        }

        public ActionResult Callback(string templateId, string dataProviderId, string dataProviderArgs)
        {
            var vm = this.VM(templateId, dataProviderId, dataProviderArgs);

            if (!string.IsNullOrEmpty(dataProviderId))
            {
                this.FillDataSource(vm.XtraReport, dataProviderId, dataProviderArgs);
            }

            return PartialView("Viewer", vm);
        }

        public ActionResult Export(string templateId, string dataProviderId, string dataProviderArgs)
        {
            var template = this.TemplateMgr.GetTemplate(templateId);

            if (!string.IsNullOrEmpty(dataProviderId))
            {
                this.FillDataSource(template.XtraReport, dataProviderId, dataProviderArgs);
            }

            return DocumentViewerExtension.ExportTo(template.XtraReport);
        }

        private ViewerVM VM(string templateId, string dataProviderId, string dataProviderArgs)
        {
            var template = this.TemplateMgr.GetTemplate(templateId);

            return new ViewerVM
            {
                TemplateID = templateId,
                TemplateName = template.TemplateName,
                DataProviderID = dataProviderId,
                DataProviderArgs = dataProviderArgs,
                XtraReport = template.XtraReport
            };
        }

        private void FillDataSource(XtraReport xtraReport, string dataProviderId, string dataProviderArgs)
        {
            var query = HttpUtility.ParseQueryString(dataProviderArgs ?? string.Empty);
            var provider = this.DataProviderMgr.GetDataProvider(dataProviderId);
            xtraReport.DataSource = provider.Entity.GetDataSource(query, false);
        }
    }
}