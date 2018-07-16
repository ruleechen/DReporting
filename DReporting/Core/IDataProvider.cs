using DevExpress.DataAccess;
using System.Collections.Specialized;

namespace DReporting.Core
{
    public interface IDataProvider
    {
        string DataProviderName { get; }

        DataComponentBase GetDataSource(NameValueCollection args, bool designTime);
    }
}
