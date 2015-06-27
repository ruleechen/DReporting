using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Composition;
using DReporting.Core;

namespace DReporting.Services
{
    [Export("dreporting.default.template", typeof(IReport))]
    public partial class DefaultXtraReport : DevExpress.XtraReports.UI.XtraReport, IReport
    {
        public DefaultXtraReport()
        {
            InitializeComponent();
        }
    }
}
