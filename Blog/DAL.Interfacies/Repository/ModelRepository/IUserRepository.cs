using DAL.Interfacies.DTO;

namespace DAL.Interfacies.Repository.ModelRepository
{
    public interface IUserRepository : IRepository<DalUser>
    {
        /// <summary>
        /// This method finds DAL user by given nickname.
        /// </summary>
        /// <param name="nickname">Nickname of searching user.</param>
        /// <returns>Returns DAL user with give nickname</returns>
        DalUser GetByNickname(string nickname);
        /// <summary>
        /// This method finds DAL user by given email.
        /// </summary>
        /// <param name="email">Email of searching user.</param>
        /// <returns>Returns DAL user with given email.</returns>
        DalUser GetByEmail(string email);
        /// <summary>
        /// This method add role to user.
        /// </summary>
        /// <param name="nickname">User's nickname.</param>
        /// <param name="roleName">Role's name.</param>
        void AddRoleToUser(string nickname, string roleName);
    }
}
