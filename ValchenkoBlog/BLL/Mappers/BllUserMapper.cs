using BLL.Interfacies.Entities;
using DAL.Interfacies.DTO;

namespace BLL.Mappers
{
    public static class BllUserMapper
    {
        public static DalUser ToDalUser(this UserEntity bllUser)
        {
            if (bllUser == null)
                return null;

            return new DalUser
            {
                Id = bllUser.Id,
                Nickname = bllUser.Nickname,
                Password = bllUser.Password,
                Avatar = bllUser.Avatar
            };
        }

        public static UserEntity ToBllUser(this DalUser dalUser)
        {
            if (dalUser == null)
                return null;

            return new UserEntity
            {
                Id = dalUser.Id,
                Nickname = dalUser.Nickname,
                Password = dalUser.Password,
                Avatar = dalUser.Avatar
            };
        }
    }
}
