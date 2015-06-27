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
        IEnumerable<DataProviderModel> QueryDataProviders(int? skip = null, int? take = null);

        DataProviderModel GetDataProvider(string dataProviderId);
    }
}
