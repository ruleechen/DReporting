using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
using DReporting.Core;
using System.Collections.Specialized;
using System.ComponentModel.Composition;

namespace DReporting.Web.ReportDatas
{
    [Export("Reporting.SqlDataProvider", typeof(IDataProvider))]
    public class SqlDataProvider : IDataProvider
    {
        public string DataProviderName
        {
            get { return "Sql Data Provider"; }
        }

        public object GetDataSource(NameValueCollection args, bool designTime)
        {
            var query = new CustomSqlQuery { Name = "Vouchers" };

            // parameter from desiginer
            var search = new QueryParameter();
            search.Name = "search";
            search.Type = typeof(DevExpress.DataAccess.Expression);
            search.Value = new DevExpress.DataAccess.Expression("[Parameters.search]", typeof(string));
            query.Parameters.Add(search);

            // parameter from runtime
            var code = new QueryParameter();
            code.Name = "code";
            code.Type = typeof(string);
            code.Value = args["code"];
            query.Parameters.Add(code);

            if (designTime)
            {
                query.Sql = "SELECT * FROM Voucher where VoucherCode = @search";
            }
            else
            {
                query.Sql = "SELECT * FROM Voucher where VoucherCode = @search or VoucherCode = @code";
            }


            // var mssqlConn = new MsSqlConnectionParameters("localhost", "nwind.mdf", "username", "password", MsSqlAuthorizationType.SqlServer);
            // var mysqlConn = new MySqlConnectionParameters("localhost", "db name", "username", "password", "port");

            var ds = new SqlDataSource("iPro");
            ds.Queries.Add(query);

            ds.RebuildResultSchema();

            return ds;
        }
    }
}