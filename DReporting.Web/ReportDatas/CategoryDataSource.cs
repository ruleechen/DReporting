using DReporting.Core;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;

namespace DReporting.Web.ReportDatas
{
    [Export("dbooking.Categories", typeof(IDataProvider))]
    public class CategoryDataSource : IDataProvider
    {
        public string DataProviderName
        {
            get { return "Categories"; }
        }

        public object GetDataSource(NameValueCollection args, bool designTime)
        {
            return new Categories();
        }
    }
}