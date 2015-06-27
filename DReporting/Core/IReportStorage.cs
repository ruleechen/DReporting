using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraReports.UI;

namespace DReporting.Core
{
    public interface IReportStorage
    {
        IDictionary<string, XtraReport> AllReports();

        XtraReport GetDefaultReport();

        XtraReport GetReport(string reportId);

        void RemoveReport(string reportId);

        void SaveReport(string reportId, byte[] xmlContext);
    }
}
