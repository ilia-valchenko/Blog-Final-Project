﻿using System;
using BLL.Interfacies.Entities;
using DAL.Interfacies.DTO;

namespace BLL.Mappers
{
    public static class BllUserMapper
    {
        public static DalUser ToDalUser(this UserEntity bllUser)
        {
            if (bllUser == null)
                throw new ArgumentNullException(nameof(bllUser));

            return new DalUser
            {
                Id = bllUser.Id,
                Nickname = bllUser.Nickname,
                Email = bllUser.Email,
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
                Email = dalUser.Email,
                Password = dalUser.Password,
                Avatar = dalUser.Avatar
            };
        }
    }
}
