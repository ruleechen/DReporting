using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraReports.UI;

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
