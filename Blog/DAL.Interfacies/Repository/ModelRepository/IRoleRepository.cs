using DAL.Interfacies.DTO;
using System.Collections.Generic;

namespace DAL.Interfacies.Repository.ModelRepository
{
    public interface IRoleRepository : IRepository<DalRole>
    {
        /// <summary>
        /// This method finds DAL role by given name.
        /// </summary>
        /// <param name="name">Name of searching role.</param>
        /// <returns>Returns DAL role with given name.</returns>
        DalRole GetRoleByName(string name);
        /// <summary>
        /// This method returns DAL roles of user by using user's id.
        /// </summary>
        /// <param name="userId">Id of the user.</param>
        /// <returns>Returns collections of DAL roles.</returns>
        IEnumerable<DalRole> GetRolesOfUser(int userId);
    }
}

