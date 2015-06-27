using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DReporting.Core
{
    public interface IReportService
    {
        IDictionary<string, IDataSource> AllDataSources();

        IDataSource GetDataSource(string dataSourceId);

        IDictionary<string, XtraReport> AllReports();

        XtraReport DefaultReportTemplate();

        XtraReport GetReport(string reportId);

        void SaveReport(string reportId, byte[] xmlContext);
    }
}
