using BLL.Interfacies.Entities;

namespace BLL.Interfacies.Services
{
    public interface IUserService : IService<UserEntity>
    {
        UserEntity GetUserEntityByNickname(string nickname);
    }
}
