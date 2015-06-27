using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DReporting.Core
{
    public interface IDataSource
    {
        string DataSourceName { get; }

        object QueryData(NameValueCollection query, bool designTime);
    }
}
