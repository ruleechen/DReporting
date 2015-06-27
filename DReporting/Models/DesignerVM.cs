using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DReporting.Core;

namespace DReporting.Models
{
    public class DesignerVM
    {
        public IReport Report { get; set; }

        public IDictionary<string, IDataSource> DataSources { get; set; }
    }
}
