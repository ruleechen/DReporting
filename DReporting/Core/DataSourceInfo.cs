using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DReporting.Core
{
    public class DataSourceInfo
    {
        public DataSourceInfo()
        {
        }

        public DataSourceInfo(string name)
            : this()
        {
            this.Name = name;
        }

        public string Name { get; set; }

        public string Category { get; set; }
    }
}
