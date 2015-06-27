using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraReports.UI;
using DReporting.Core;
using DReporting.Services;

namespace DReporting
{
    public static class Consts
    {
        public const string AreaName = "Reporting";

        public static byte[] GetBuffer(this XtraReport xtraReport)
        {
            using (var reportStream = new MemoryStream())
            {
                xtraReport.SaveLayout(reportStream);
                return reportStream.GetBuffer();
            }
        }

        public static IReportDatas ReportDatas
        {
            get
            {
                return InjectContainer.Instance.GetExport<IReportDatas>();
            }
        }

        public static IReportStorage ReportStorage
        {
            get
            {
                return InjectContainer.Instance.GetExport<IReportStorage>();
            }
        }
    }
}
