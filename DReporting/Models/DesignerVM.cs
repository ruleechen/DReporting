using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DReporting.Core;
using DevExpress.XtraReports.Web.ReportDesigner;

namespace DReporting.Models
{
    public class DesignerVM
    {
        public string ReportId { get; set; }

        public string ReportName { get; set; }

        public ReportDesignerModel DesignerModel { get; set; }
    }
}
