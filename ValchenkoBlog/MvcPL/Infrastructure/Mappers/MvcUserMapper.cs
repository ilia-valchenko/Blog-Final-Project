using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcPL.Models;
using BLL.Interfacies.Entities;

namespace MvcPL.Infrastructure.Mappers
{
    public static class MvcUserMapper
    {
        public static UserViewModel ToMvcUser(this UserEntity bllUser)
        {
            if (bllUser == null)
                return null;

            return new UserViewModel
            {
                Id = bllUser.Id,
                Nickname = bllUser.Nickname
                // ?password
                // ?avatar
            };
        }

        public static UserEntity ToBllUser(this UserViewModel mvcUser)
        {
            if (mvcUser == null)
                return null;

            return new UserEntity 
            {
                Id = mvcUser.Id,
                Nickname = mvcUser.Nickname
                // ?password
                // ?avatar
            };
        }
    }
}