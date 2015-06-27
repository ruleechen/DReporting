using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DReporting.Web.Mvc.ViewModels
{
    public class ReportVM
    {
        public string ReportID { get; set; }

        public string ReportCode { get; set; }

        public string ReportName { get; set; }

        public string CategoryID { get; set; }

        public string CreationTime { get; set; }

        public string LastUpdateTime { get; set; }
    }
}
