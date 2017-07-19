using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace CheckbookBuddy.IdentityHelpers
{
    public class CustomRoleProvider : RoleProvider
    {
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            using (var usersContext = new CheckingContext())
            {
                var user = usersContext.Users.SingleOrDefault(u => u.UserName == username);
                if (user == null)
                    return false;
                return user.Role != null && user.Role.ToString() == roleName;
            }
        }

        public override string[] GetRolesForUser(string username)
        {
            using (var usersContext = new CheckingContext())
            {
                var user = usersContext.Users.SingleOrDefault(u => u.UserName == username);
                if (user == null)
                    return new string[] { };
                return user.Role == null ? new string[] { } :
                 new string[] { user.Role.ToString() };
            }
        }

        // -- Snip --

        public override string[] GetAllRoles()
        {
            return Enum.GetNames(typeof(Role));
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        // -- Snip --
    }
}