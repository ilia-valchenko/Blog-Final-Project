using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Interfacies.DTO;
using DAL.Interfacies.Repository.ModelRepository;
using ORM.Models;
using DAL.Mappers;
using System.Data.Entity;

namespace DAL.Concrete.ModelRepository
{
    /// <summary>
    /// Realization of IUserRepository interface.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        public UserRepository(DbContext context)
        {
            this.context = context;
        }

        #region CRUD operations
        public void Create(DalUser entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Role role = context.Set<Role>().FirstOrDefault(r => r.Name == "user");

            var user = entity.ToOrmUser();
            user.Roles.Add(role);

            context.Set<User>().Add(user);
        }

        public void Update(DalUser entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var user = context.Set<User>().SingleOrDefault(u => u.UserId == entity.Id);

            if (user != null)
            {
                user.Nickname = entity.Nickname;
                user.Password = entity.Password;
                user.Avatar = entity.Avatar;
            }
        }

        public void Delete(DalUser entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var user = context.Set<User>().SingleOrDefault(u => u.UserId == entity.Id);

            if (user != null)
                context.Set<User>().Remove(user);
        }
        #endregion

        #region Get operations
        public DalUser GetById(int key) => context.Set<User>().FirstOrDefault(user => user.UserId == key)?.ToDalUser();
        public DalUser GetByNickname(string nickname) => context.Set<User>().FirstOrDefault(u => u.Nickname == nickname)?.ToDalUser();
        public DalUser GetByEmail(string email) => context.Set<User>().FirstOrDefault(u => u.Email == email)?.ToDalUser();
        public IEnumerable<DalUser> GetAll() => context.Set<User>().ToList().Select(u => u.ToDalUser());
        #endregion

        /// <summary>
        /// This method add role to user.
        /// </summary>
        /// <param name="nickname">User's nickname.</param>
        /// <param name="roleName">Role's name.</param>
        public void AddRoleToUser(string nickname, string roleName)
        {
            if (nickname == null)
                throw new ArgumentNullException(nameof(nickname));

            if (roleName == null)
                throw new ArgumentNullException(nameof(roleName));

            var user = context.Set<User>().FirstOrDefault(u => u.Nickname == nickname);

            if (user != null)
            {
                var role = context.Set<Role>().FirstOrDefault(r => r.Name == roleName);

                if (role != null)
                    user.Roles.Add(role);
            }
        }

        private readonly DbContext context;
    }
}
