using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DReporting.Core;

namespace DReporting.Services
{
    [Export(typeof(IReportDatas))]
    public class DefaultReportDatas : IReportDatas
    {
        public IDictionary<string, IDataSource> AllDataSources()
        {
            var metas = InjectContainer.Instance.ExportMetas();
            var objs = InjectContainer.Instance.GetExports<IDataSource>();
            return objs.ToDictionary(x => metas.First(m => m.ComponentType == x.GetType()).ContractName, x => x);
        }

        public IDataSource GetDataSource(string dataSourceId)
        {
            return InjectContainer.Instance.GetExport<IDataSource>(dataSourceId);
        }
    }
}
