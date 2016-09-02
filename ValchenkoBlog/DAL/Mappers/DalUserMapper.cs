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

            //return new DalUser
            var dalUser = new DalUser
            {
                Id = ormUser.UserId,
                Nickname = ormUser.Nickname,
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
                return null;

            return new User
            {
                Nickname = dalUser.Nickname,
                Password = dalUser.Password,
                Avatar = dalUser.Avatar
            };
        }
    }
}
