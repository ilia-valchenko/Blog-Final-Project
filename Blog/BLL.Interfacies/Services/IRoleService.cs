using BLL.Interfacies.Entities;
using System.Collections.Generic;

namespace BLL.Interfacies.Services
{
    public interface IRoleService : IService<RoleEntity>
    {
        /// <summary>
        /// This method finds BLL role by given id of user.
        /// </summary>
        /// <param name="userId">Id of the user.</param>
        /// <returns>Returns collection of DAL roles.</returns>
        IEnumerable<RoleEntity> GetRolesOfUser(int userId);
    }
}

