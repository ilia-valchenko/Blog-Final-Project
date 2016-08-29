using DAL.Interfacies.DTO;

namespace DAL.Interfacies.Repository.ModelRepository
{
    public interface IUserRepository : IRepository<DalUser>
    {
        DalUser GetByNickname(string nickname);
        void AddRoleToUser(int userId, string roleName);
    }
}
