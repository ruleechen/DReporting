using System.Collections.Specialized;

namespace DReporting.Core
{
    public interface IDataProvider
    {
        string DataProviderName { get; }

        object GetDataSource(NameValueCollection args, bool designTime);
    }
}
