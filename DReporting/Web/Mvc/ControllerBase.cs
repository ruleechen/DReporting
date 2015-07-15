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
        private ITemplateMgr _templateMgr;
        public ITemplateMgr TemplateMgr
        {
            get
            {
                return _templateMgr ?? (_templateMgr = ReportingGlobal.ReportStorage);
            }
        }

        private IDataProviderMgr _dataProviderMgr;
        public IDataProviderMgr DataProviderMgr
        {
            get
            {
                return _dataProviderMgr ?? (_dataProviderMgr = ReportingGlobal.ReportStorage);
            }
        }

        private ICategoryMgr _categoryMgr;
        public ICategoryMgr CategoryMgr
        {
            get
            {
                return _categoryMgr ?? (_categoryMgr = ReportingGlobal.ReportStorage);
            }
        }
    }
}
