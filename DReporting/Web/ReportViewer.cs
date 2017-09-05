using DevExpress.XtraReports.Web;
using DReporting.Web.Mvc.ViewModels;
using System;
using System.Web.Mvc;

namespace DReporting.Web
{
    public abstract class ReportViewer : ViewUserControl<ViewerVM>
    {
        public abstract ASPxDocumentViewer GetViewer();

        protected void Page_Load(object sender, EventArgs e)
        {
            var viewer = GetViewer();
            viewer.Report = Model.XtraReport;
        }
    }
}
