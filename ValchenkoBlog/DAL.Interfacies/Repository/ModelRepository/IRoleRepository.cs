using DAL.Interfacies.DTO;
using System.Collections.Generic;

namespace DAL.Interfacies.Repository.ModelRepository
{
    public interface IRoleRepository : IRepository<DalRole>
    {
        DalRole GetRoleByName(string name);
        //void AddUserToRole(int userId, string roleName);
        IEnumerable<DalRole> GetRolesOfUser(int userId);
    }
}

