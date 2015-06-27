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
        public override string AreaName
        {
            get
            {
                return Consts.AreaName;
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            WebResources.RegisterBundles(BundleTable.Bundles);

            context.MapRoute(
                this.AreaName + "_default",
                this.AreaName + "/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "DReporting.Web.Area.Controllers" }
            );
        }
    }
}
