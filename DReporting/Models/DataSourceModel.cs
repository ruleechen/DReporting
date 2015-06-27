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
        public string DataSourceID { get; set; }

        public IDataSource InnerDataSource { get; set; }
    }
}
