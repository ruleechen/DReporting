using DevExpress.XtraReports.UI;
using DReporting.Services;
using System.IO;

namespace DReporting
{
    public static class Extensions
    {
        public static string GetXML(this XtraReport xtraReport)
        {
            using (var reportStream = new MemoryStream())
            {
                xtraReport.SaveLayout(reportStream);

                reportStream.Position = 0;

                using (var streamReader = new StreamReader(reportStream))
                {
                    var content = streamReader.ReadToEnd();

                    var baseClass = typeof(BaseXtraReport).FullName;

                    content = content.Replace(": DevExpress.XtraReports.UI.XtraReport {", ": " + baseClass + " {");

                    return content;
                }
            }
        }
    }
}
