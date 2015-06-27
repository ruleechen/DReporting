using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DReporting.Models;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Web;

namespace DReporting.Web
{
    public abstract class ReportViewer : ViewUserControl<ViewerVM>
    {
        public abstract ASPxDocumentViewer GetViewer();

        protected void Page_Load(object sender, EventArgs e)
        {
            var viewer = this.GetViewer();
            viewer.Report = this.Model.Report;
        }
    }
}
