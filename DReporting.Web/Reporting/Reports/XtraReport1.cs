using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Composition;
using DReporting.Core;

namespace DReporting.Web.Reporting.Reports
{
    [Export("dbooking.reporting.report.test", typeof(IReport))]
    public partial class XtraReport1 : DevExpress.XtraReports.UI.XtraReport, IReport
    {
        public XtraReport1()
        {
            InitializeComponent();
        }
    }
}
