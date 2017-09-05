using System.Web.Mvc;
using System.Web.Optimization;

namespace DReporting.Web.Mvc
{
    public class AreaRegistration : System.Web.Mvc.AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return ReportingGlobal.AreaName;
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            ReportingGlobal.ApplicationStart();
            WebResources.RegisterBundles(BundleTable.Bundles);

            context.MapRoute(
                AreaName + "_default",
                AreaName + "/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "DReporting.Web.Mvc.Controllers" }
            );
        }
    }
}
