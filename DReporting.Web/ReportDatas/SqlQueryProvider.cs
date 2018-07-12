using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
using DReporting.Core;
using System.Collections.Specialized;
using System.ComponentModel.Composition;

namespace DReporting.Web.ReportDatas
{
    [Export("Reporting.SqlQueryProvider", typeof(IDataProvider))]
    public class SqlQueryProvider : IDataProvider
    {
        public string DataProviderName
        {
            get { return "SqlQueryProvider"; }
        }

        public object GetDataSource(NameValueCollection args, bool designTime)
        {
            var query = new CustomSqlQuery { Name = "Vouchers" };

            // parameter from desiginer
            var code = new QueryParameter();
            code.Name = "code";
            code.Type = typeof(DevExpress.DataAccess.Expression);
            code.Value = new DevExpress.DataAccess.Expression("[Parameters.code]", typeof(string));
            query.Parameters.Add(code);

            // parameter from runtime
            var code1 = new QueryParameter();
            code1.Name = "code1";
            code1.Type = typeof(string);
            code1.Value = args["code"];
            query.Parameters.Add(code1);

            if (designTime)
            {
                query.Sql = "SELECT * FROM Voucher where VoucherCode = @code";
            }
            else
            {
                query.Sql = "SELECT * FROM Voucher where VoucherCode = @code or VoucherCode = @code1";
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