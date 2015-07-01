using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DReporting.Core;
using DReporting.Services;

namespace DReporting.Web.Mvc
{
    public abstract class ControllerBase : Controller
    {
        private IReportStorage _reportStorage;
        public IReportStorage ReportStorage
        {
            get
            {
                return _reportStorage ?? (_reportStorage = ReportingContext.ReportStorage);
            }
        }
    }
}
