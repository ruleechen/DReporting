using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DReporting.Services
{
    public class BaseXtraReport : DevExpress.XtraReports.UI.XtraReport
    {
        protected override void OnDataSourceDemanded(EventArgs e)
        {
            base.OnDataSourceDemanded(e);
        }
    }
}
