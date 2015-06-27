using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Composition;
using DReporting.Core;

namespace DReporting.Web.Reports
{
    [Export("dbooking.report.test", typeof(IReport))]
    public partial class XtraReport1 : DevExpress.XtraReports.UI.XtraReport, IReport
    {
        public ReportInfo ReportInfo
        {
            get
            {
                return new ReportInfo("Test");
            }
        }

        public XtraReport1()
        {
            InitializeComponent();
        }
    }
}
