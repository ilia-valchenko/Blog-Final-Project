using System;
using BLL.Interfacies.Entities;
using MvcPL.Models.User;

namespace MvcPL.Infrastructure.Mappers
{
    public static class MvcUserMapper
    {
        public static UserProfileViewModel ToMvcUser(this UserEntity userEntity)
        {
            if (userEntity == null)
                return null;

            return new UserProfileViewModel()
            {
                Id = userEntity.Id,
                Nickname = userEntity.Nickname,
                Email = userEntity.Email,
                Avatar = userEntity.Avatar
            };
        }

        public static UserEntity ToBllUser(this UserProfileViewModel userViewModel)
        {
            if (userViewModel == null)
                throw new ArgumentNullException(nameof(userViewModel));

            return new UserEntity()
            {
                Id = userViewModel.Id,
                Nickname = userViewModel.Nickname,
                Email = userViewModel.Email,
            };
        }
    }
}