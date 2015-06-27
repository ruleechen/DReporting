using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DReporting.Core
{
    public interface IReportService
    {
        IDictionary<string, IReport> AllReports();

        IDictionary<string, IDataSource> AllDataSources();

        IReport DefaultReportTemplate();

        IReport GetReport(string reportId);

        IDataSource GetDataSource(string dataSourceId);

        void SaveReport(string reportId, byte[] xmlContext);
    }
}
