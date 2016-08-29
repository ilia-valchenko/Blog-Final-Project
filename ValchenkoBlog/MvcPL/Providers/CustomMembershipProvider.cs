using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Security;
using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;

namespace MvcPL.Providers
{
    // This provider helps 
    public class CustomMembershipProvider : MembershipProvider
    {
        // mine

        public IUserService UserService
        {
            get { return (IUserService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserService));}
        }

        // ? Role service

        public MembershipUser CreateUser(string nickname, string password /* avatar */)
        {
            var user = new UserEntity
            {
                Nickname = nickname,
                Password = Crypto.HashPassword(password),
            };

            UserService.Create(user);
            var membershipUser = GetUser(nickname, false);
            return membershipUser;
        }

        public override MembershipUser GetUser(string nickname, bool userIsOnline)
        {
            var user = UserService.GetUserEntityByNickname(nickname);

            if(user == null)
                return null;

            var memberUser = new MembershipUser(
                "CustomMembershipProvider", // Provider name
                user.Nickname, // Name
                null, // Provider user key
                null, // Email
                null, // Password question
                null, // Comment
                false, // Is approved
                false, // Is locked out
                default(DateTime), // Creation date
                DateTime.MinValue, //Last login date
                DateTime.MinValue, // Last activity date
                DateTime.MinValue, // Last password changed date
                DateTime.MinValue); // Last lockout date

            return memberUser;
        }

        public override bool ValidateUser(string nickname, string password)
        {
            var user = UserService.GetUserEntityByNickname(nickname);

            if (user != null && Crypto.VerifyHashedPassword(user.Password, password))
            //Определяет, соответствуют ли заданный хэш RFC 2898 и пароль друг другу
            {
                return true;
            }
            return false;
        }

        #region Not implemented methods
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

        public override bool EnablePasswordReset
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool EnablePasswordRetrieval
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int MinRequiredPasswordLength
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int PasswordAttemptWindow
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override string PasswordStrengthRegularExpression
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool RequiresUniqueEmail
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
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

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}