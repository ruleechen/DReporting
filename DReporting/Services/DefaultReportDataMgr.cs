using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DReporting.Core;
using DReporting.Models;

namespace DReporting.Services
{
    [Export(typeof(IReportDataMgr))]
    public class DefaultReportDataMgr : IReportDataMgr
    {
        public IEnumerable<DataProviderModel> QueryDataProviders(int? skip = null, int? take = null)
        {
            var metas = InjectContainer.Instance.ExportMetas();

            var providers = InjectContainer.Instance.GetExports<IDataProvider>();

            var query = providers.Select(x => new DataProviderModel
            {
                DataProviderID = metas.First(m => m.ComponentType == x.GetType()).ContractName,
                Entity = x
            });

            query = query.OrderBy(x => x.DataProviderID);

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

        public DataProviderModel GetDataProvider(string dataProviderId)
        {
            var provider = InjectContainer.Instance.GetExport<IDataProvider>(dataProviderId);

            return new DataProviderModel
            {
                DataProviderID = dataProviderId,
                Entity = provider
            };
        }
    }
}
