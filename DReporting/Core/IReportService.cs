using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DReporting.Core
{
    public interface IReportService
    {
        IEnumerable<IReport> AllReports();

        IEnumerable<IDataSource> AllDataSources();

        IReport GetReport(string reportId);

        IDataSource GetDataSource(string dataSourceId);
    }
}
