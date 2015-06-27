using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraReports.UI;

namespace DReporting.Web.Mvc.ViewModels
{
    public class DesignerVM
    {
        public string ReportID { get; set; }

        public string ReportName { get; set; }

        public XtraReport XtraReport { get; set; }

        public IDictionary<string, object> DataSources { get; set; }

        //public DevExpress.XtraReports.Web.ReportDesigner.ReportDesignerModel DesignerModel { get; set; }
    }
}
