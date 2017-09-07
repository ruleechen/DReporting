using DevExpress.DataAccess.ObjectBinding;
using DReporting.Core;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Composition;

namespace DReporting.Web.ReportDatas
{
    [Export("Reporting.ObjectProviderPlus", typeof(IDataProvider))]
    public class ObjectProviderPlus : IDataProvider
    {
        public string DataProviderName
        {
            get { return "ObjectProviderPlus"; }
        }

        public object GetDataSource(NameValueCollection args, bool designTime)
        {
            var users = new Users();
            users.Add(new User
            {
                FirstName = "RuleePlus",
                LastName = "Chen",
                Role = new Role { Group = "R&D" },
                Roles = new Roles { new Role { Group = "Yundang" } },
            });

            var ds = new ObjectDataSource();

            ds.DataSource = users;

            ds.Fill();

            return ds;
        }

        [DisplayName("Users")]
        [HighlightedClass]
        public class Users : List<User>
        {
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

            [HighlightedMember]
            public Role Role { get; set; }

            [HighlightedMember]
            public Roles Roles { get; set; }
        }

        [DisplayName("RolesPlus")]
        [HighlightedClass]
        public class Roles : List<Role>
        {
        }

        [DisplayName("Role")]
        [HighlightedClass]
        public class Role
        {
            [HighlightedMember]
            public string Group { get; set; }
        }
    }
}