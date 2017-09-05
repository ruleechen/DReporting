using DReporting.Core;
using System.Web.Mvc;

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
