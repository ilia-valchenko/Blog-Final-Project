using System;
using DAL.Interfacies.DTO;
using ORM.Models;

namespace DAL.Mappers
{
    /// <summary>
    /// This static class provides extension methods for DAL role entity.
    /// </summary>
    public static class DalRoleMapper
    {
        /// <summary>
        /// This method maps ORM role to DAL role.
        /// </summary>
        /// <param name="ormRole">ORM role.</param>
        /// <returns>DAL role.</returns>
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

        /// <summary>
        /// This method maps DAL role to ORM role.
        /// </summary>
        /// <param name="dalRole">DAL role.</param>
        /// <returns>ORM role.</returns>
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
