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
        public UserEntity GetUserEntityByNickname(string nickname) => userRepository.GetByNickname(nickname)?.ToBllUser();
        public UserEntity GetUserEntityByEmail(string email) => userRepository.GetByEmail(email)?.ToBllUser();
        #endregion

        public IEnumerable<RoleEntity> GetRolesOfUser(int userId) => roleRepository.GetRolesOfUser(userId).Select(r => r.ToBllRole());

        public void ChangeAvatar(string nickname, HttpPostedFileBase file)
        {
            if (nickname == null)
                throw new ArgumentNullException(nameof(nickname));

            if (file == null)
                throw new ArgumentNullException(nameof(file));

            if (file.ContentLength < 0)
                throw new Exception("Content length of file is less then zero.");

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
