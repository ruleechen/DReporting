using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DReporting.Models;

namespace DReporting.Core
{
    public interface IReportStorage
    {
        IEnumerable<ReportModel> QueryReports(int? skip = null, int? take = null);

        ReportModel GetDefaultReport();

        ReportModel GetReport(string reportId);

        void RemoveReport(string reportId);

        void SaveReport(string reportId, byte[] xmlContext);
    }
}
