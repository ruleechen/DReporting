using System;

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
