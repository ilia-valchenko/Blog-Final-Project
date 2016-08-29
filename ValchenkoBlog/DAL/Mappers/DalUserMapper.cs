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

            return new DalUser
            {
                Id = ormUser.UserId,
                Nickname = ormUser.Nickname,
                Password = ormUser.Password,
                Avatar = ormUser.Avatar
            };
        }

        public static User ToOrmUser(this DalUser dalUser)
        {
            if (dalUser == null)
                return null;

            return new User
            {
                // without UserId
                Nickname = dalUser.Nickname,
                Password = dalUser.Password,
                Avatar = dalUser.Avatar
            };
        }
    }
}
