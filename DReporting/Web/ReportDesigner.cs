using DevExpress.XtraReports.Web;
using DReporting.Web.Mvc.ViewModels;
using System;
using System.Web.Mvc;

namespace DReporting.Web
{
    public abstract class ReportDesigner : ViewUserControl<DesignerVM>
    {
        public abstract ASPxReportDesigner GetDesigner();

        protected override void OnInit(EventArgs e)
        {
            var designer = GetDesigner();
            designer.SaveReportLayout += designer_SaveReportLayout;
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var designer = GetDesigner();
                //TODO:
            }
        }

        void designer_SaveReportLayout(object sender, SaveReportLayoutEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
