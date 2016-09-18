using System.Web;
using BLL.Interfacies.Entities;

namespace BLL.Interfacies.Services
{
    public interface IUserService : IService<UserEntity>
    {
        /// <summary>
        /// This method finds BLL user by given nickname.
        /// </summary>
        /// <param name="nickname">Nickname of searching user.</param>
        /// <returns>Returns BLL user with given nickname.</returns>
        UserEntity GetUserEntityByNickname(string nickname);
        /// <summary>
        /// This method finds BLL user by given email.
        /// </summary>
        /// <param name="email">Email of searching user.</param>
        /// <returns>Returns BLL user with given email.</returns>
        UserEntity GetUserEntityByEmail(string email);
        /// <summary>
        /// This method change user's avatar.
        /// </summary>
        /// <param name="nickname">Nickname of user whose avatar you want to change.</param>
        /// <param name="file">New avatar.</param>
        void ChangeAvatar(string nickname, HttpPostedFileBase file);
    }
}
