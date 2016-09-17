using BLL.Interfacies.Entities;
using System.Collections.Generic;

namespace BLL.Interfacies.Services
{
    public interface IRoleService : IService<RoleEntity>
    {
        IEnumerable<RoleEntity> GetRolesOfUser(int userId);
    }
}

