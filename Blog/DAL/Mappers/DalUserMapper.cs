using System;
using DAL.Interfacies.DTO;
using ORM.Models;

namespace DAL.Mappers
{
    /// <summary>
    /// This static class provides extension methods for DAL user entity.
    /// </summary>
    public static class DalUserMapper
    {
        /// <summary>
        /// This method maps ORM user to DAL user.
        /// </summary>
        /// <param name="ormUser">ORM user.</param>
        /// <returns>DAL user.</returns>
        public static DalUser ToDalUser(this User ormUser)
        {
            if (ormUser == null)
                return null;

            var dalUser = new DalUser
            {
                Id = ormUser.UserId,
                Nickname = ormUser.Nickname,
                Email = ormUser.Email,
                Password = ormUser.Password,
                Avatar = ormUser.Avatar,
            };

            return dalUser;
        }

        /// <summary>
        /// This method maps DAL user to ORM user.
        /// </summary>
        /// <param name="dalUser">DAL user.</param>
        /// <returns>ORM user.</returns>
        public static User ToOrmUser(this DalUser dalUser)
        {
            if (dalUser == null)
                throw new ArgumentNullException(nameof(dalUser));

            return new User
            {
                Nickname = dalUser.Nickname,
                Email = dalUser.Email,
                Password = dalUser.Password,
                Avatar = dalUser.Avatar
            };
        }
    }
}
