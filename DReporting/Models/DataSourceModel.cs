using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DReporting.Core;

namespace DReporting.Models
{
    public class DataSourceModel
    {
        public string DataSourceId { get; set; }

        public string DataSourceName { get; set; }

        public IDataSource DataSource { get; set; }
    }
}
