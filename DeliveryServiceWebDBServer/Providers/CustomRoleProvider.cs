using DeliveryServiceWebDBServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace DeliveryServiceWebDBServer.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            string[] roles = new string[] { };
            using (ModelDBContainer db = new ModelDBContainer())
            {
                // Получаем пользователя
                User user = db.Users.FirstOrDefault(u => u.Login == username);
                if (user != null)
                {
                    string role = "";
                    // назначаем роль
                    if (user.Admin) role = "admin";
                    else role = "user";
                    roles = new string[] { role };
                }
                return roles;
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            using (ModelDBContainer db = new ModelDBContainer())
            {
                // Получаем пользователя
                User user = db.Users.FirstOrDefault(u => u.Login == username);

                if (user != null)
                {
                    // получаем роль
                    string role = "";
                    if (user.Admin) role = "admin";
                    else role = "user";

                    if (role == roleName) return true;
                    else return false;
                }
                    return false;
            }
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}