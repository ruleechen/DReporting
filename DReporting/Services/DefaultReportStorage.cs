using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using DevExpress.XtraReports.UI;
using DReporting.Core;
using DReporting.Models;

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

        public IEnumerable<ReportModel> QueryReports(int? skip = null, int? take = null)
        {
            var reports = Directory.GetFiles(ReportsDir, "*.xml");

            var query = reports.Select(x => new
            {
                file = new FileInfo(x),
                report = XtraReport.FromFile(x, true)
            })
            .Select(x => new ReportModel
            {
                ReportId = Path.GetFileNameWithoutExtension(x.file.Name),
                ReportName = x.report.DisplayName,
                XtraReport = x.report,
                CreationTime = x.file.CreationTimeUtc,
                LastUpdateTime = x.file.LastWriteTimeUtc
            });

            query = query.OrderByDescending(x => x.CreationTime);

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query;
        }

        public ReportModel GetDefaultReport()
        {
            var template = new DefaultXtraReport();

            return new ReportModel
            {
                ReportId = null,
                ReportName = template.DisplayName,
                CreationTime = DateTime.Now,
                LastUpdateTime = null,
                XtraReport = template
            };
        }

        public ReportModel GetReport(string reportId)
        {
            var file = Path.Combine(ReportsDir, reportId + ".xml");

            var info = new FileInfo(file);

            var template = XtraReport.FromFile(file, true);

            return new ReportModel
            {
                ReportId = reportId,
                ReportName = template.DisplayName,
                CreationTime = info.CreationTimeUtc,
                LastUpdateTime = info.LastWriteTimeUtc,
                XtraReport = template
            };
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
