using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DReporting.Models;

namespace DReporting.Core
{
    public interface IReportDataMgr
    {
        IEnumerable<DataSourceModel> QueryDataSources(int? skip = null, int? take = null);

        DataSourceModel GetDataSource(string dataSourceId);
    }
}
