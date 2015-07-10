using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Optimization;

namespace DReporting.Web.Mvc
{
    public class WebResources
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            var scripts = new ScriptBundle("~/reporting/js/bundle")
                .Include(string.Format("~/Areas/{0}/Scripts/jquery-1.11.1.min.js", ReportingGlobal.AreaName))
                .Include(string.Format("~/Areas/{0}/Scripts/jquery-ui.min.js", ReportingGlobal.AreaName))
                .Include(string.Format("~/Areas/{0}/Scripts/knockout-3.3.0.js", ReportingGlobal.AreaName))
                .Include(string.Format("~/Areas/{0}/Scripts/globalize.js", ReportingGlobal.AreaName))
                .Include(string.Format("~/Areas/{0}/Scripts/ace.js", ReportingGlobal.AreaName))
                .Include(string.Format("~/Areas/{0}/Scripts/ext-language_tools.js", ReportingGlobal.AreaName))
                .Include(string.Format("~/Areas/{0}/Scripts/dreporting.js", ReportingGlobal.AreaName));
            scripts.Orderer = new ScriptsOrderer();
            bundles.Add(scripts);

            bundles.Add(new StyleBundle("~/reporting/css/bundles")
                .Include(string.Format("~/Areas/{0}/Content/jquery-ui.min.css", ReportingGlobal.AreaName))
                .Include(string.Format("~/Areas/{0}/Content/bootstrap.css", ReportingGlobal.AreaName))
                .Include(string.Format("~/Areas/{0}/Content/dreporting.css", ReportingGlobal.AreaName)));
        }

        public class ScriptsOrderer : IBundleOrderer
        {
            public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
            {
                return files;
            }
        }
    }
}
