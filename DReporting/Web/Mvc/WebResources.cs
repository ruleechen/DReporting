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
            bundles.Add(new ScriptBundle("~/reporting/js/bundle").Include(
                string.Format("~/Areas/{0}/Scripts/jquery-1.11.1.min.js", Contextual.AreaName),
                string.Format("~/Areas/{0}/Scripts/jquery-ui.min.js", Contextual.AreaName),
                string.Format("~/Areas/{0}/Scripts/knockout-3.3.0.js", Contextual.AreaName),
                string.Format("~/Areas/{0}/Scripts/globalize.js", Contextual.AreaName),
                string.Format("~/Areas/{0}/Scripts/dreporting.js", Contextual.AreaName)
            ));

            bundles.Add(new StyleBundle("~/reporting/css/bundles").Include(
                string.Format("~/Areas/{0}/Content/bootstrap.css", Contextual.AreaName),
                string.Format("~/Areas/{0}/Content/dreporting.css", Contextual.AreaName)
            ));
        }
    }
}
