using System;
using DAL.Interfacies.DTO;
using ORM.Models;

namespace DAL.Mappers
{
    public static class DalUserMapper
    {
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

            /*foreach (var ormRole in ormUser.Roles)
                dalUser.Roles.Add(ormRole.ToDalRole());*/

            return dalUser;
        }

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
