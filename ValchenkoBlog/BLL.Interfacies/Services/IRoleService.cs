using BLL.Interfacies.Entities;
using System.Collections.Generic;

namespace BLL.Interfacies.Services
{
    public interface IRoleService : IService<RoleEntity>
    {
        void AddUserToRole(int userId, string roleName);
        IEnumerable<RoleEntity> GetRolesOfUser(int userId);
    }
}

