using System;
using System.IO;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using DAL.Interfacies.Repository;
using DAL.Interfacies.Repository.ModelRepository;
using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;
using BLL.Mappers;

namespace BLL.Services
{
    /// <summary>
    /// Realization of IUserService interface.
    /// </summary>
    public class UserService : IUserService
    {
        public UserService(IUnitOfWork unitOfWork,
                           IUserRepository userRepository,
                           IRoleRepository roleRepository)
        {
            this.unitOfWork = unitOfWork;
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
        }

        #region CRUD operations
        public void Create(UserEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            userRepository.Create(entity.ToDalUser());
            unitOfWork.Commit();
        }

        public void Update(UserEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            userRepository.Update(entity.ToDalUser());
            unitOfWork.Commit();
        }

        public void Delete(UserEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            userRepository.Delete(entity.ToDalUser());
            unitOfWork.Commit();
        }
        #endregion

        #region Get operations
        public IEnumerable<UserEntity> GetAll() => userRepository.GetAll().Select(u => u.ToBllUser());
        public UserEntity GetById(int id) => userRepository.GetById(id)?.ToBllUser();
        /// <summary>
        /// This method finds BLL user by given nickname.
        /// </summary>
        /// <param name="nickname">Nickname of searching user.</param>
        /// <returns>Returns BLL user with given nickname.</returns>
        public UserEntity GetUserEntityByNickname(string nickname) => userRepository.GetByNickname(nickname)?.ToBllUser();
        /// <summary>
        /// This method finds BLL user by given email.
        /// </summary>
        /// <param name="email">Email of searching user.</param>
        /// <returns>Returns BLL user with given email.</returns>
        public UserEntity GetUserEntityByEmail(string email) => userRepository.GetByEmail(email)?.ToBllUser();
        #endregion

        public IEnumerable<RoleEntity> GetRolesOfUser(int userId) => roleRepository.GetRolesOfUser(userId).Select(r => r.ToBllRole());

        /// <summary>
        /// This method change user's avatar.
        /// </summary>
        /// <param name="nickname">Nickname of user whose avatar you want to change.</param>
        /// <param name="file">New avatar.</param>
        public void ChangeAvatar(string nickname, HttpPostedFileBase file)
        {
            if (nickname == null)
                throw new ArgumentNullException(nameof(nickname));

            if (file == null)
                throw new ArgumentNullException(nameof(file));

            if (file.ContentLength < 0)
                throw new Exception("Content length of file is less then zero.");
                // log it

            var user = userRepository.GetByNickname(nickname);

            if (user != null)
            {
                byte[] avatar;

                using (Stream inputStream = file.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;

                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }

                    avatar = memoryStream.ToArray();
                }

                user.Avatar = avatar;
                userRepository.Update(user);
                unitOfWork.Commit();
            }
        }

        private readonly IUnitOfWork unitOfWork;
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;
    }
}
