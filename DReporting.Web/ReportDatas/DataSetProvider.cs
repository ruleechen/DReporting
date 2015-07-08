using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.Linq;
using DReporting.Core;

namespace DReporting.Web.ReportDatas
{
    [Export("Reporting.DataSetProvider", typeof(IDataProvider))]
    public class DataSetProvider : IDataProvider
    {
        public string DataProviderName
        {
            get { return "DataSetProvider"; }
        }

        public object GetDataSource(NameValueCollection args, bool designTime)
        {
            if (designTime)
            {
                return new DataSet1();
            }
            else
            {
                var ds = new DataSet1();

                var row0 = ds.DataTable1.NewDataTable1Row();
                row0.DataColumn1 = "TEST 0";
                ds.DataTable1.AddDataTable1Row(row0);

                var row1 = ds.DataTable1.NewDataTable1Row();
                row1.DataColumn1 = "TEST 1";
                ds.DataTable1.AddDataTable1Row(row1);

                return ds;
            }
        }
    }
}