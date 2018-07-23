using DevExpress.DataAccess;
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

        public DataComponentBase GetDataSource(NameValueCollection args, bool designMode)
        {
            var designModeParameter = new QueryParameter();
            designModeParameter.Name = "designMode";
            designModeParameter.Type = typeof(bool);
            designModeParameter.Value = designMode;

            // parameter from desiginer
            var search = new QueryParameter();
            search.Name = "search";
            search.Type = typeof(DevExpress.DataAccess.Expression);
            search.Value = new DevExpress.DataAccess.Expression("[Parameters.search]", typeof(string));

            // parameter from runtime
            var code = new QueryParameter();
            code.Name = "code";
            code.Type = typeof(string);
            code.Value = args["code"];

            var query = new CustomSqlQuery { Name = "Vouchers" };
            query.Parameters.Add(designModeParameter);
            query.Parameters.Add(search);
            query.Parameters.Add(code);

            if (designMode)
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