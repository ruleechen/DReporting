using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DReporting.Core
{
    public interface IReportDatas
    {
        IDictionary<string, IDataSource> AllDataSources();

        IDataSource GetDataSource(string dataSourceId);
    }
}
