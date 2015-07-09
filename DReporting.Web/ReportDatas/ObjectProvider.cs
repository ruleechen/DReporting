using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using DevExpress.DataAccess.ObjectBinding;
using DReporting.Core;

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
            var users = new Users();
            users.Add(new User
            {
                FirstName = "Ronglin",
                LastName = "Chen",
                Role = new Role { Group = "Yaitoo" },
                Roles = new Roles { new Role { Group = "Yaitoo" } },
            });


            var ds = new ObjectDataSource();

            ds.DataSource = users;

            ds.Fill();

            return ds;
        }
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
        [HighlightedMember]
        public string FirstName { get; set; }

        [HighlightedMember]
        public string LastName { get; set; }

        [HighlightedMember]
        public Role Role { get; set; }

        [HighlightedMember]
        public Roles Roles { get; set; }
    }

    [DisplayName("Roles")]
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