using System;
using System.Web.Helpers;
using System.Web.Security;
using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;

namespace MvcPL.Providers
{
    public class CustomMembershipProvider : MembershipProvider
    {
        public IUserService UserService => (IUserService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserService));
        public IRoleService RoleService => (IRoleService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRoleService));

        public MembershipUser CreateUser(string email, string nickname, string password)
        {
            /*MembershipUser membershipUser = GetUser(email, false);

            if (membershipUser != null)
                return null;*/

            var bllUser = new UserEntity
            {
                Email = email,
                Nickname = nickname,
                Password = Crypto.HashPassword(password),
                // Avatar
            };

            UserService.Create(bllUser);
            var membershipUser = GetUser(nickname, false);
            return membershipUser;
        }

        public override bool ValidateUser(string nickname, string password)
        {
            var user = UserService.GetUserEntityByNickname(nickname);

            if (user != null && Crypto.VerifyHashedPassword(user.Password, password))
                return true;

            return false;
        }

        public override MembershipUser GetUser(string nickname, bool userIsOnline)
        {
            var user = UserService.GetUserEntityByNickname(nickname);

            if (user == null)
                return null;

            return new MembershipUser(
                "CustomMembershipProvider", // providerName
                user.Nickname, // name 
                null, // providerUserKey
                null, // email
                null, // passwordQuestion
                null, // comment
                false, // isApproved
                false, // isLockedOut
                default(DateTime), // creationDate
                DateTime.MinValue, // lastLoginDate
                DateTime.MinValue, // lastActivityDate
                DateTime.MinValue, // lastPasswordChangedDate
                DateTime.MinValue); // lastLockoutDate
        }

        public override string GetUserNameByEmail(string email)
        {
            var user = UserService.GetUserEntityByEmail(email);

            if (user == null)
                return null;

            var memberUser = new MembershipUser(
                "CustomMembershipProvider", 
                user.Nickname,
                null, 
                null, 
                null, 
                null,
                false, 
                false, 
                default(DateTime),
                DateTime.MinValue, 
                DateTime.MinValue,
                DateTime.MinValue, 
                DateTime.MinValue);

            return memberUser.UserName;
        }

        #region Not implemented methods
        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer,
            bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion,
            string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordRetrieval { get; }
        public override bool EnablePasswordReset { get; }
        public override bool RequiresQuestionAndAnswer { get; }
        public override string ApplicationName { get; set; }
        public override int MaxInvalidPasswordAttempts { get; }
        public override int PasswordAttemptWindow { get; }
        public override bool RequiresUniqueEmail { get; }
        public override MembershipPasswordFormat PasswordFormat { get; }
        public override int MinRequiredPasswordLength { get; }
        public override int MinRequiredNonAlphanumericCharacters { get; }
        public override string PasswordStrengthRegularExpression { get; } 
        #endregion
    }
}