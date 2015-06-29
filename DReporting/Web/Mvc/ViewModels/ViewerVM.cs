﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraReports.UI;

namespace DReporting.Web.Mvc.ViewModels
{
    public class ViewerVM
    {
        public string TemplateID { get; set; }

        public string TemplateName { get; set; }

        public XtraReport XtraReport { get; set; }
    }
}
