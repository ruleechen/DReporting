using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Optimization;

namespace DReporting.Web.Area
{
    public class WebResources
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js/reporting").Include(
                string.Format("~/Areas/{0}/Scripts/jquery-1.11.1.min.js", Consts.AreaName),
                string.Format("~/Areas/{0}/Scripts/jquery-ui.min.js", Consts.AreaName),
                string.Format("~/Areas/{0}/Scripts/knockout-3.3.0.js", Consts.AreaName),
                string.Format("~/Areas/{0}/Scripts/globalize.js", Consts.AreaName),
                string.Format("~/Areas/{0}/Scripts/dreporting.js", Consts.AreaName)
            ));

            bundles.Add(new StyleBundle("~/bundles/css/reporting").Include(
                string.Format("~/Areas/{0}/Content/bootstrap.css", Consts.AreaName),
                string.Format("~/Areas/{0}/Content/dreporting.css", Consts.AreaName)
            ));
        }
    }
}
