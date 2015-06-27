using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DReporting.Core;

namespace DReporting.Web.Area
{
    public class ControllerBase : Controller
    {
        private IReportService _reportService;
        public IReportService ReportService
        {
            get
            {
                return _reportService ?? (_reportService = DReporting.Services.Container.Instance.ResolveValue<IReportService>());
            }
        }
    }
}
