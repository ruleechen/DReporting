using DReporting.Core;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;

namespace DReporting.Web.ReportDatas
{
    [Export("dbooking.Categories", typeof(IDataSource))]
    public class CategoryDataSource : IDataSource
    {
        public string DataSourceName
        {
            get { return "Categories"; }
        }

        public object QueryData(NameValueCollection query, bool designTime)
        {
            return new Categories();
        }
    }
}