using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using BLL.Interfacies.Services;
using MvcPL.Infrastructure.Mappers;
using BLL.Interfacies.Entities;
using MvcPL.Models;

namespace MvcPL.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        public IUserService UserService => (IUserService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserService));
        public IRoleService RoleService => (IRoleService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRoleService));

        public override bool IsUserInRole(string nickname, string roleName)
        {
            var user = UserService.GetUserEntityByNickname(nickname).ToMvcUser();

            if (user == null)
                return false;

            IEnumerable<RoleEntity> userRoles = RoleService.GetRolesOfUser(user.Id);

            if (userRoles == null)
                return false;

            if (userRoles.Any(role => role.Name == roleName))
                return true;

            return false;
        }

        public override string[] GetRolesForUser(string nickname)
        {
            var roles = new string[] { };
            var user = UserService.GetUserEntityByNickname(nickname).ToMvcUser();

            if (user == null)
                return roles;

            var userRoles = RoleService.GetRolesOfUser(user.Id);

            if (userRoles == null)
                return roles;

            return userRoles.Select(role => role.Name).ToArray();
        }

        public override void CreateRole(string roleName)
        {
            RoleService.Create(new RoleEntity() { Name = roleName });
        }

        #region Not implemented methods
        public override string ApplicationName { get; set; }
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

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        } 
        #endregion
   
    }
}