using DevExpress.DataAccess;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace DReporting.Web.Mvc.ViewModels
{
    public class DesignerVM
    {
        public string TemplateID { get; set; }

        public string TemplateName { get; set; }

        public XtraReport XtraReport { get; set; }

        public IDictionary<string, DataComponentBase> DataSources { get; set; }

        //public DevExpress.XtraReports.Web.ReportDesigner.ReportDesignerModel DesignerModel { get; set; }
    }
}
