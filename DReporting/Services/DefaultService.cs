using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DReporting.Core;

namespace DReporting.Services
{
    [Export(typeof(IReportService))]
    public class DefaultService : IReportService
    {
        public IEnumerable<IReport> AllReports()
        {
            return Container.Instance.ResolveValues<IReport>();
        }

        public IEnumerable<IDataSource> AllDataSources()
        {
            return Container.Instance.ResolveValues<IDataSource>();
        }

        public IReport GetReport(string reportId)
        {
            return Container.Instance.ResolveValue<IReport>(reportId);
        }

        public IDataSource GetDataSource(string dataSourceId)
        {
            return Container.Instance.ResolveValue<IDataSource>(dataSourceId);
        }
    }
}
