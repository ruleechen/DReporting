using DevExpress.XtraReports.UI;
using System.IO;

namespace DReporting
{
    public static class Extensions
    {
        public static byte[] GetBuffer(this XtraReport xtraReport)
        {
            using (var reportStream = new MemoryStream())
            {
                xtraReport.SaveLayout(reportStream);
                return reportStream.GetBuffer();
            }
        }
    }
}
