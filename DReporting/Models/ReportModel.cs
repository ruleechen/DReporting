using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraReports.UI;

namespace DReporting.Models
{
    public class ReportModel
    {
        public string ReportId { get; set; }

        public string ReportName { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? LastUpdateTime { get; set; }

        public XtraReport XtraReport { get; set; }
    }
}
