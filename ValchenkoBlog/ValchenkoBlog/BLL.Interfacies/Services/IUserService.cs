using System.Web;
using BLL.Interfacies.Entities;

namespace BLL.Interfacies.Services
{
    public interface IUserService : IService<UserEntity>
    {
        UserEntity GetUserEntityByNickname(string nickname);
        UserEntity GetUserEntityByEmail(string email);
        void ChangeAvatar(string nickname, HttpPostedFileBase file);
    }
}
