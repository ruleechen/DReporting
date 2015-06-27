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
        private IReportDataMgr _reportDataMgr;
        public IReportDataMgr ReportDataMgr
        {
            get
            {
                return _reportDataMgr ?? (_reportDataMgr = InjectContainer.Instance.GetExport<IReportDataMgr>());
            }
        }

        private IReportStorage _reportStorage;
        public IReportStorage ReportStorage
        {
            get
            {
                return _reportStorage ?? (_reportStorage = InjectContainer.Instance.GetExport<IReportStorage>());
            }
        }
    }
}
