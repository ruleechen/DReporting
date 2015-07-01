using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                return ReportingContext.AreaName;
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            ReportingContext.App_Start();
            WebResources.RegisterBundles(BundleTable.Bundles);

            context.MapRoute(
                this.AreaName + "_default",
                this.AreaName + "/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "DReporting.Web.Mvc.Controllers" }
            );
        }
    }
}
