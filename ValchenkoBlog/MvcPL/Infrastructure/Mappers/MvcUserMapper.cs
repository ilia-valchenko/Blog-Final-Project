using BLL.Interfacies.Entities;
using MvcPL.Models.User;

namespace MvcPL.Infrastructure.Mappers
{
    public static class MvcUserMapper
    {
        public static UserViewModel ToMvcUser(this UserEntity userEntity)
        {
            return new UserViewModel()
            {
                Id = userEntity.Id,
                Nickname = userEntity.Nickname
                // Avatar
            };
        }

        public static UserEntity ToBllUser(this UserViewModel userViewModel)
        {
            return new UserEntity()
            {
                Id = userViewModel.Id,
                Nickname = userViewModel.Nickname,
                //Password = userViewModel.Password
                Password = "Hardcode_Qwerty_123"
            };
        }
    }
}