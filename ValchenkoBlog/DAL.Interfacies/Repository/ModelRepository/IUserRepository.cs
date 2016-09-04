using DAL.Interfacies.DTO;

namespace DAL.Interfacies.Repository.ModelRepository
{
    public interface IUserRepository : IRepository<DalUser>
    {
        DalUser GetByNickname(string nickname);
        DalUser GetByEmail(string email);
        void AddRoleToUser(string nickname, string roleName);
    }
}
