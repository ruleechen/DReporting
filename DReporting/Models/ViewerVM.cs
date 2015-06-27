using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DReporting.Core;
using DevExpress.XtraReports.UI;

namespace DReporting.Models
{
    public class ViewerVM
    {
        public string ReportId { get; set; }

        public XtraReport Report { get; set; }
    }
}
