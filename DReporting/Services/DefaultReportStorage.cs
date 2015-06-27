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
    [Export(typeof(IReportStorage))]
    public class DefaultReportStorage : IReportStorage
    {
        static string ReportsDir;
        static DefaultReportStorage()
        {
            ReportsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "Reports");
            if (!Directory.Exists(ReportsDir)) { Directory.CreateDirectory(ReportsDir); }
        }

        public IDictionary<string, XtraReport> AllReports()
        {
            var reports = Directory.GetFiles(ReportsDir, "*.xml");
            return reports.Select(x => new
            {
                file = new FileInfo(x),
                report = XtraReport.FromFile(x, true)
            })
            .OrderByDescending(x => x.file.LastWriteTimeUtc)
            .ToDictionary(x => Path.GetFileNameWithoutExtension(x.file.Name), x => x.report);
        }

        public XtraReport GetDefaultReport()
        {
            return new DefaultXtraReport();
        }

        public XtraReport GetReport(string reportId)
        {
            var file = Path.Combine(ReportsDir, reportId + ".xml");
            return XtraReport.FromFile(file, true);
        }

        public void RemoveReport(string reportId)
        {
            var file = Path.Combine(ReportsDir, reportId + ".xml");
            File.Delete(file);
        }

        public void SaveReport(string reportId, byte[] xmlContext)
        {
            var file = Path.Combine(ReportsDir, reportId + ".xml");
            File.WriteAllBytes(file, xmlContext);
        }
    }
}
