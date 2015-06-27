using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DReporting.Core;
using DevExpress.XtraReports.UI;

namespace DReporting.Models
{
    public class HomeVM
    {
        public IEnumerable<ReportModel> Reports { get; set; }

        public IEnumerable<DataSourceModel> DataSources { get; set; }
    }
}
