using System;
using BLL.Interfacies.Entities;
using DAL.Interfacies.DTO;

namespace BLL.Mappers
{
    public static class BllRoleMapper
    {
        public static DalRole ToDalRole(this RoleEntity bllRole)
        {
            if (bllRole == null)
                throw new ArgumentNullException(nameof(bllRole));

            return new DalRole
            {
                Id = bllRole.Id,
                Name = bllRole.Name
            };
        }

        public static RoleEntity ToBllRole(this DalRole dalRole)
        {
            if (dalRole == null)
                return null;

            return new RoleEntity
            {
                Id = dalRole.Id,
                Name = dalRole.Name
            };
        }
    }
}
