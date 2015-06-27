using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DReporting.Core
{
    public interface IDataProvider
    {
        string DataProviderName { get; }

        object GetDataSource(NameValueCollection args, bool designTime);
    }
}
