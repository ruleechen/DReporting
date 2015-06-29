using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraReports.UI;
using DReporting.Core;
using DReporting.Services;

namespace DReporting
{
    public static class ReportContext
    {
        public const string AreaName = "Reporting";

        public static byte[] GetBuffer(this XtraReport xtraReport)
        {
            using (var reportStream = new MemoryStream())
            {
                xtraReport.SaveLayout(reportStream);
                return reportStream.GetBuffer();
            }
        }

        internal static void App_Start()
        {
            // Use Project's Web.config Connection Strings
            DevExpress.XtraReports.Web.ReportDesigner.DefaultReportDesignerContainer
                .RegisterDataSourceWizardConfigFileConnectionStringsProvider();
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
