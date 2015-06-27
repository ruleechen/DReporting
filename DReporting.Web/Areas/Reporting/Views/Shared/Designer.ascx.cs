using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraReports.Web;

namespace DReporting.Web.Areas.Reporting.Views.Shared
{
    public partial class Designer : DReporting.Web.ReportDesigner
    {
        public override ASPxReportDesigner GetDesigner()
        {
            return this.ASPxReportDesigner1;
        }
    }
}