using System;
using DAL.Interfacies.DTO;
using ORM.Models;

namespace DAL.Mappers
{
    public static class DalRoleMapper
    {
        public static DalRole ToDalRole(this Role ormRole)
        {
            if (ormRole == null)
                return null;

            return new DalRole
            {
                Id = ormRole.RoleId,
                Name = ormRole.Name
            };
        }

        public static Role ToOrmRole(this DalRole dalRole)
        {
            if (dalRole == null)
                throw new ArgumentNullException(nameof(dalRole));

            return new Role
            {
                Name = dalRole.Name
            };
        }
    }
}
