using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using DevExpress.XtraReports.UI;
using DReporting.Core;
using DReporting.Services;

namespace DReporting
{
    public static class ReportingContext
    {
        public const string AreaName = "Reporting";

        public static void ApplicationStart()
        {
            // Use Project's Web.config Connection Strings
            DevExpress.XtraReports.Web.ReportDesigner.DefaultReportDesignerContainer
                .RegisterDataSourceWizardConfigFileConnectionStringsProvider();

            // Register inject assembilies
            InjectContainer.RegisterAssembiles(AppDomain.CurrentDomain.GetAssemblies()
                .Where(i => i.FullName.StartsWith("DReporting", StringComparison.InvariantCultureIgnoreCase)));
        }

        public static void RegisterInject(IEnumerable<Assembly> assemblies)
        {
            InjectContainer.RegisterAssembiles(assemblies);
        }

        public static IReportStorage ReportStorage
        {
            get
            {
                return InjectContainer.Instance.GetExport<IReportStorage>();
            }
        }
    }
}
