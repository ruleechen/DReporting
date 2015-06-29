﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DReporting.Core;
using DevExpress.XtraReports.UI;

namespace DReporting.Web.Mvc.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<TemplateVM> Templates { get; set; }

        public IEnumerable<CategoryVM> Categories { get; set; }

        public IEnumerable<DataProviderVM> DataProviders { get; set; }
    }
}
