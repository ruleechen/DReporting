using DevExpress.XtraReports.UI;
using System;

namespace DReporting.Models
{
    public class TemplateModel
    {
        public string TemplateID { get; set; }

        public string TemplateCode { get; set; }

        public string TemplateName { get; set; }

        public string CategoryID { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? LastUpdateTime { get; set; }

        public XtraReport XtraReport { get; set; }
    }
}
