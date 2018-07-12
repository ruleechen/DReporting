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
    [Export("Reporting.ObjectProvider", typeof(IDataProvider))]
    public class ObjectProvider : IDataProvider
    {
        public string DataProviderName
        {
            get { return "ObjectProvider"; }
        }

        public object GetDataSource(NameValueCollection args, bool designTime)
        {
            // parameter from desiginer
            var userNameParameter = new Parameter();
            userNameParameter.Name = "userName";
            userNameParameter.Type = typeof(DevExpress.DataAccess.Expression);
            userNameParameter.Value = new DevExpress.DataAccess.Expression("[Parameters.UserName]", typeof(string));

            // parameter from runtime
            var userNameParameter1 = new Parameter();
            userNameParameter1.Name = "userName1";
            userNameParameter1.Type = typeof(string);
            userNameParameter1.Value = args["userName"];

            var ds = new ObjectDataSource();
            ds.Constructor = new ObjectConstructorInfo(userNameParameter, userNameParameter1);
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
        private string _userName;
        private string _userName1;

        public UserData(string userName, string userName1)
        {
            _userName = userName;
            _userName1 = userName1;
        }

        public IReadOnlyList<User> GetUserList()
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
                    FirstName = "Winston",
                    LastName = "Xie",
                },
            };

            return users.Where(x => x.FirstName == _userName || x.FirstName == _userName1).ToList();
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