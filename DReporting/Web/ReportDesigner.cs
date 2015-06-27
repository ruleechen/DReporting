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
    public abstract class ReportDesigner : ViewUserControl<DesignerVM>
    {
        public abstract ASPxReportDesigner GetDesigner();

        protected void Page_Load(object sender, EventArgs e)
        {
            var designer = this.GetDesigner();

            designer.SaveReportLayout += designer_SaveReportLayout;

            if (!this.IsPostBack)
            {
                if (Model.DataSources != null && Model.DataSources.Count > 0)
                {
                    foreach (var item in Model.DataSources)
                    {
                        designer.DataSources.Add(item.Key, item.Value);
                    }
                }

                designer.OpenReport((XtraReport)Model.Report);
            }
        }

        void designer_SaveReportLayout(object sender, SaveReportLayoutEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
