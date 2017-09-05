using DReporting.Models;
using System.Collections.Generic;

namespace DReporting.Core
{
    public interface IDataProviderMgr
    {
        IEnumerable<DataProviderModel> QueryDataProviders(int? skip = null, int? take = null);

        DataProviderModel GetDataProvider(string dataProviderId);

        DataProviderModel SaveDataProvider(DataProviderModel model);
    }
}
