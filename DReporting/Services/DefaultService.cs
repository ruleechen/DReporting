using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using DReporting.Core;
using DevExpress.XtraReports.UI;

namespace DReporting.Services
{
    [Export(typeof(IReportService))]
    public class DefaultService : IReportService
    {
        static string ReportsDir;
        static DefaultService()
        {
            ReportsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reporting", "Reports");
            if (!Directory.Exists(ReportsDir)) { Directory.CreateDirectory(ReportsDir); }
        }

        public IDictionary<string, IDataSource> AllDataSources()
        {
            var metas = Container.Instance.ExportMetas();
            var objs = Container.Instance.ResolveValues<IDataSource>();
            return objs.ToDictionary(x => metas.First(m => m.ComponentType == x.GetType()).ContractName, x => x);
        }

        public IDataSource GetDataSource(string dataSourceId)
        {
            return Container.Instance.ResolveValue<IDataSource>(dataSourceId);
        }


        public IDictionary<string, XtraReport> AllReports()
        {
            var reports = Directory.GetFiles(ReportsDir, "*.xml");
            return reports.Select(x => new { file = x, report = XtraReport.FromFile(x, true) }).ToDictionary(x => x.file, x => x.report);
        }

        public XtraReport DefaultReportTemplate()
        {
            return new DefaultXtraReport();
        }

        public XtraReport GetReport(string reportId)
        {
            var file = Path.Combine(ReportsDir, reportId + ".xml");
            return XtraReport.FromFile(file, true);
        }

        public void SaveReport(string reportId, byte[] xmlContext)
        {
            if (string.IsNullOrEmpty(reportId))
            {
                throw new ArgumentNullException("reportId");
            }

            var dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reporting", "Reports");
            if (!Directory.Exists(dir)) { Directory.CreateDirectory(dir); }
            var file = Path.Combine(dir, reportId + ".xml");
            File.WriteAllBytes(file, xmlContext);
        }
    }
}
