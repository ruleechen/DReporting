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

            if (!string.IsNullOrEmpty(dataProviderId))
            {
                var query = HttpUtility.ParseQueryString(Request.Url.Query);
                var provider = this.ReportStorage.GetDataProvider(dataProviderId);
                var dataSource = provider.Entity.GetDataSource(query, false);

                var sqlDataSource = dataSource as DevExpress.DataAccess.Sql.SqlDataSource;
                if (sqlDataSource != null)
                {
                    var sqlQuery = sqlDataSource.Queries.OfType<DevExpress.DataAccess.Sql.CustomSqlQuery>().FirstOrDefault();
                    template.XtraReport.DataMember = sqlQuery != null ? sqlQuery.Name : sqlDataSource.Name;
                    template.XtraReport.DataSource = sqlDataSource;
                }
                else
                {
                    template.XtraReport.DataSource = dataSource;
                }
            }

            return new ViewerVM
            {
                TemplateID = templateId,
                TemplateName = template.TemplateName,
                DataProviderID = dataProviderId,
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