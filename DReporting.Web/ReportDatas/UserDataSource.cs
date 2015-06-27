using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using DReporting.Core;

namespace DReporting.Web.ReportDatas
{
    [Export("dbooking.UserDataSource", typeof(IDataProvider))]
    public class UserDataSource : IDataProvider
    {
        public string DataProviderName
        {
            get
            {
                return "User Data Source";
            }
        }

        public object GetDataSource(NameValueCollection args, bool designTime)
        {
            var users = new UserCollection();
            users.Add(new User
            {
                FirstName = "Ronglin",
                LastName = "Chen",
                Role = new Role { Group = "Yaitoo" },
                Roles = new RoleCollection { new Role { Group = "Yaitoo" } },
            });

            return users;
        }
    }

    public class UserCollection : List<User>
    {
    }

    public class User
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Role Role { get; set; }

        public RoleCollection Roles { get; set; }
    }

    public class RoleCollection : List<Role>
    {
    }

    public class Role
    {
        public string Group { get; set; }
    }
}