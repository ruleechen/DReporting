using DReporting.Core;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;

namespace DReporting.Web.ReportDatas
{
    [Export("Reporting.DataSetDataProvider", typeof(IDataProvider))]
    public class DataSetDataProvider : IDataProvider
    {
        public string DataProviderName
        {
            get { return "DataSet"; }
        }

        public object GetDataSource(NameValueCollection args, bool designTime)
        {
            return new Categories();
        }
    }
}