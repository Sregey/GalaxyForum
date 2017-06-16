using ForumBll.Interface.Models;
using ForumBll.Interface.Services;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ForumPlMvc.Providers
{
    public class ForumRoleProvider : RoleProvider
    {
        private IUserService userService;

        public ForumRoleProvider()
        {
            userService = DependencyResolver.Current.GetService<IUserService>();
        }

        public ForumRoleProvider(IUserService userService)
        {
            this.userService = userService;
        }

        //[Inject]
        //public void UserService(IUserService userService)
        //{
        //    this.userService = userService;
        //}

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
            BllUser user = userService.GetUser(username);
            if (user != null)
                return new string[] { user.Role.Name };
            else
                return new string[] { };
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            BllUser user = userService.GetUser(username);
            if (user != null)
                return user.Role.Name == roleName;
            else
                return false;
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