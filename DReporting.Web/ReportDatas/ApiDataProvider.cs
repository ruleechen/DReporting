using DevExpress.DataAccess;
using DevExpress.DataAccess.ObjectBinding;
using DReporting.Core;
using System;
using System.Collections.Specialized;
using System.ComponentModel.Composition;

namespace DReporting.Web.ReportDatas
{
    [Export("Reporting.ApiDataProvider", typeof(IDataProvider))]
    public class ApiDataProvider : IDataProvider
    {
        public string DataProviderName
        {
            get
            {
                return "Api Data Provider";
            }
        }

        public DataComponentBase GetDataSource(NameValueCollection args, bool designMode)
        {
            var designModeParameter = new Parameter();
            designModeParameter.Name = "designMode";
            designModeParameter.Type = typeof(bool);
            designModeParameter.Value = designMode;

            // parameter from desiginer
            var search = new Parameter();
            search.Name = "search";
            search.Type = typeof(DevExpress.DataAccess.Expression);
            search.Value = new DevExpress.DataAccess.Expression("[Parameters.search]", typeof(string));

            // parameter from runtime
            var contactName = new Parameter();
            contactName.Name = "contactName";
            contactName.Type = typeof(string);
            contactName.Value = args["contactName"];

            var ds = new ObjectDataSource();
            ds.Constructor = new ObjectConstructorInfo(designModeParameter, search, contactName);
            // ds.Parameters.Add(designModeParameter);
            // ds.Parameters.Add(userNameParameter);
            // ds.Parameters.Add(userNameParameter1);
            ds.DataSource = typeof(DynamicApiData);
            ds.DataMember = "GetApiData";

            // ds.Fill();

            return ds;
        }
    }

    public class DynamicApiData
    {
        private bool _designMode;
        private string _search;
        private string _contactName;

        public DynamicApiData(bool designMode, string search, string contactName)
        {
            _designMode = designMode;
            _search = search;
            _contactName = contactName;
        }

        public System.Data.DataSet GetApiData()
        {
            var table = new System.Data.DataTable("Booking");
            table.Columns.Add(new System.Data.DataColumn("BookingID", typeof(int)));
            table.Columns.Add(new System.Data.DataColumn("ContactName", typeof(string)));
            table.Columns.Add(new System.Data.DataColumn("CheckInDate", typeof(DateTime)));
            table.Columns.Add(new System.Data.DataColumn("CheckOutDate", typeof(DateTime)));

            var row1 = table.NewRow();
            row1["BookingID"] = 1;
            row1["ContactName"] = "Rulee";
            row1["CheckInDate"] = DateTime.Today;
            row1["CheckOutDate"] = DateTime.Today.AddDays(1);
            table.Rows.Add(row1);

            var row2 = table.NewRow();
            row2["BookingID"] = 2;
            row2["ContactName"] = "Cerrie";
            row2["CheckInDate"] = DateTime.Today;
            row2["CheckOutDate"] = DateTime.Today.AddDays(1);
            table.Rows.Add(row2);

            if (!_designMode)
            {
                var row3 = table.NewRow();
                row3["BookingID"] = 3;
                row3["ContactName"] = "Lex";
                row3["CheckInDate"] = DateTime.Today;
                row3["CheckOutDate"] = DateTime.Today.AddDays(1);
                table.Rows.Add(row3);
            }

            var ds = new System.Data.DataSet();
            ds.Tables.Add(table);
            return ds;
        }
    }
}