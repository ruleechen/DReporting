using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DReporting.Core;

namespace DReporting.Models
{
    public class DataProviderModel
    {
        public string DataProviderID { get; set; }

        public string CategoryID { get; set; }

        public IDataProvider Entity { get; set; }
    }
}
