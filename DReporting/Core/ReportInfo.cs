using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DReporting.Core
{
    public class ReportInfo
    {
        public ReportInfo()
        {
        }

        public ReportInfo(string name)
            : this()
        {
            this.Name = name;
        }

        public string Name { get; set; }

        public string Category { get; set; }
    }
}
