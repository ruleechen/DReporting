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
        public IEnumerable<DataSourceModel> QueryDataSources(int? skip = null, int? take = null)
        {
            var metas = InjectContainer.Instance.ExportMetas();

            var objs = InjectContainer.Instance.GetExports<IDataSource>();

            var query = objs.Select(x => new DataSourceModel
            {
                DataSourceID = metas.First(m => m.ComponentType == x.GetType()).ContractName,
                InnerDataSource = x
            });

            query = query.OrderBy(x => x.DataSourceID);

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

        public DataSourceModel GetDataSource(string dataSourceId)
        {
            var dataSource = InjectContainer.Instance.GetExport<IDataSource>(dataSourceId);

            return new DataSourceModel
            {
                DataSourceID = dataSourceId,
                InnerDataSource = dataSource
            };
        }
    }
}
