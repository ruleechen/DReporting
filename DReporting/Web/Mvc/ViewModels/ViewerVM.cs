using DevExpress.XtraReports.UI;

namespace DReporting.Web.Mvc.ViewModels
{
    public class ViewerVM
    {
        public string TemplateID { get; set; }

        public string TemplateName { get; set; }

        public string DataProviderID { get; set; }

        public string DataProviderArgs { get; set; }

        public XtraReport XtraReport { get; set; }
    }
}
