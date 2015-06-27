using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using DReporting.Core;
using DevExpress.DataAccess.ConnectionParameters;

namespace DReporting.Web.ReportDatas
{
    [Export("Reporting.SampleDataProvider", typeof(IDataProvider))]
    public class SampleDataProvider : IDataProvider
    {
        public string DataProviderName
        {
            get { return "Sample"; }
        }

        public object GetDataSource(NameValueCollection args, bool designTime)
        {
            var query = new DevExpress.DataAccess.Sql.CustomSqlQuery();
            query.Name = "Vouchers";
            query.Sql = "SELECT * FROM Voucher";

            //var mssqlConn = new MsSqlConnectionParameters("localhost", "nwind.mdf", "username", "password", MsSqlAuthorizationType.SqlServer);
            //var mysqlConn = new MySqlConnectionParameters("localhost", "db name", "username", "password", "port");

            var ds = new DevExpress.DataAccess.Sql.SqlDataSource("iPro");
            ds.Queries.Add(query);

            if (designTime)
            {
                ds.RebuildResultSchema();
            }
            else
            {
                ds.Fill();
            }

            return ds;
        }
    }
}