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
        private IReportService _service;
        public IReportService Service
        {
            get
            {
                return _service ?? (_service = DReporting.Services.Container.Instance.ResolveValue<IReportService>());
            }
        }
    }
}
