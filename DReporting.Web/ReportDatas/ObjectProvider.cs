using DevExpress.DataAccess.ObjectBinding;
using DReporting.Core;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Composition;

namespace DReporting.Web.ReportDatas
{
    [Export("Reporting.ObjectProvider", typeof(IDataProvider))]
    public class ObjectProvider : IDataProvider
    {
        public string DataProviderName
        {
            get { return "ObjectProvider"; }
        }

        public object GetDataSource(NameValueCollection args, bool designTime)
        {
            var users = new List<User>
            {
                new User
                {
                    FirstName = "Rulee",
                    LastName = "Chen",
                    Role = new Role { Group = "R&D" },
                    Roles = new List<Role> { new Role { Group = "Yundang" } },
                }
            };

            var ds = new ObjectDataSource();

            ds.DataSource = users;

            // ds.Fill();

            return ds;
        }
    }

    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Role Role { get; set; }
        public List<Role> Roles { get; set; }
    }

    public class Role
    {
        public string Group { get; set; }
    }
}