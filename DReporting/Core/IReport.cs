using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DReporting.Core
{
    public interface IReport
    {
        string Name { get; set; }

        string DisplayName { get; set; }
    }
}
