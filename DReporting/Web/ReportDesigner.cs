using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Web;
using DReporting.Models;
using DReporting.Web.Mvc.ViewModels;

namespace DReporting.Web
{
    public abstract class ReportDesigner : ViewUserControl<DesignerVM>
    {
        public abstract ASPxReportDesigner GetDesigner();

        protected override void OnInit(EventArgs e)
        {
            var designer = this.GetDesigner();
            designer.SaveReportLayout += designer_SaveReportLayout;
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                var designer = this.GetDesigner();
                //TODO:
            }
        }

        void designer_SaveReportLayout(object sender, SaveReportLayoutEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
