using DevExpress.DataAccess;
using DevExpress.DataAccess.ObjectBinding;
using DReporting.Core;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;

namespace DReporting.Web.ReportDatas
{
    /// <summary>
    /// https://www.devexpress.com/Support/Center/Question/Details/T565074/end-user-reports-passing-parameters-to-objectdatasource
    /// </summary>
    [Export("Reporting.ObjectDataProvider", typeof(IDataProvider))]
    public class ObjectDataProvider : IDataProvider
    {
        public string DataProviderName
        {
            get { return "Object Data Provider"; }
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
            var userName = new Parameter();
            userName.Name = "userName";
            userName.Type = typeof(string);
            userName.Value = args["userName"];

            var ds = new ObjectDataSource();
            ds.Constructor = new ObjectConstructorInfo(designModeParameter, search, userName);
            // ds.Parameters.Add(designModeParameter);
            // ds.Parameters.Add(userNameParameter);
            // ds.Parameters.Add(userNameParameter1);
            ds.DataSource = typeof(UserData);
            ds.DataMember = "GetUserList";

            // ds.Fill();

            return ds;
        }
    }

    public class UserData
    {
        private bool _designMode;
        private string _search;
        private string _userName;

        public UserData(bool designMode, string search, string userName)
        {
            _designMode = designMode;
            _search = search;
            _userName = userName;
        }

        public List<User> GetUserList()
        {
            var users = new List<User>
            {
                new User
                {
                    FirstName = "Rulee",
                    LastName = "Chen",
                },
                new User
                {
                    FirstName = "Cerrie",
                    LastName = "Shen",
                },
            };

            return users.Where(x => x.FirstName.Contains(_search) || x.FirstName == _userName).ToList();
        }
    }

    [DisplayName("User")]
    [HighlightedClass]
    public class User
    {
        [DisplayName("名")]
        [HighlightedMember]
        public string FirstName { get; set; }
        [DisplayName("姓")]
        [HighlightedMember]
        public string LastName { get; set; }
    }
}