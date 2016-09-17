using DAL.Interfacies.DTO;
using System.Collections.Generic;

namespace DAL.Interfacies.Repository.ModelRepository
{
    public interface IRoleRepository : IRepository<DalRole>
    {
        DalRole GetRoleByName(string name);
        IEnumerable<DalRole> GetRolesOfUser(int userId);
    }
}

