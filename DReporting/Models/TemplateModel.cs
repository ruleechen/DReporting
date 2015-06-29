using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraReports.UI;

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
