using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Optimization;

namespace DReporting.Web.Area
{
    public class AreaRegistration : System.Web.Mvc.AreaRegistration
    {
        public const string ReportingAreaName = "Reporting";

        public override string AreaName
        {
            get
            {
                return ReportingAreaName;
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            WebResources.RegisterBundles(BundleTable.Bundles);

            context.MapRoute(
                "Reporting_default",
                this.AreaName + "/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "DReporting.Web.Area.Controllers" }
            );
        }
    }
}
