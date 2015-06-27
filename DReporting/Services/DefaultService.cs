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
        public IDictionary<string, IReport> AllReports()
        {
            var metas = Container.Instance.ExportMetas();
            var objs = Container.Instance.ResolveValues<IReport>();
            return objs.ToDictionary(x => metas.First(m => m.ComponentType == x.GetType()).ContractName, x => x);
        }

        public IDictionary<string, IDataSource> AllDataSources()
        {
            var metas = Container.Instance.ExportMetas();
            var objs = Container.Instance.ResolveValues<IDataSource>();
            return objs.ToDictionary(x => metas.First(m => m.ComponentType == x.GetType()).ContractName, x => x);
        }

        public IReport DefaultReportTemplate()
        {
            return Container.Instance.ResolveValue<IReport>("dreporting.default.template");
        }

        public IReport GetReport(string reportId)
        {
            return Container.Instance.ResolveValue<IReport>(reportId);
        }

        public IDataSource GetDataSource(string dataSourceId)
        {
            return Container.Instance.ResolveValue<IDataSource>(dataSourceId);
        }

        public void SaveReport(string reportId, byte[] xmlContext)
        {
            throw new NotImplementedException();
        }
    }
}
