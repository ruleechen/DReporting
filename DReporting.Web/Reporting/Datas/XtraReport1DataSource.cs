using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using DReporting.Core;

namespace DReporting.Web.Reporting.Datas
{
    [Export("dbooking.reporting.data.test", typeof(IDataSource))]
    public class XtraReport1DataSource : IDataSource
    {
    }
}