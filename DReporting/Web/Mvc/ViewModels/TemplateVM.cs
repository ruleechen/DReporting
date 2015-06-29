using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DReporting.Web.Mvc.ViewModels
{
    public class TemplateVM
    {
        public string TemplateID { get; set; }

        public string TemplateCode { get; set; }

        public string TemplateName { get; set; }

        public string CategoryID { get; set; }

        public string CreationTime { get; set; }

        public string LastUpdateTime { get; set; }
    }
}
