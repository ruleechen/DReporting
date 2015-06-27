using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraReports.Web.ReportDesigner;

namespace DReporting.Web.Mvc.ViewModels
{
    public class DesignerVM
    {
        public string ReportID { get; set; }

        public string ReportName { get; set; }

        public ReportDesignerModel DesignerModel { get; set; }
    }
}
