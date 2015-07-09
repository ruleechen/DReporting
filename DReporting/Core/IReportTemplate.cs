using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DReporting.Core
{
    public delegate void ReportInitEventHandler(object sender, ReportInitEventArgs e);

    public interface IReportTemplate
    {
        event ReportInitEventHandler OnInitializeComponent;
    }

    public class ReportInitEventArgs : EventArgs
    {

    }
}
