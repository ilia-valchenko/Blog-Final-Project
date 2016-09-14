using System;
using System.IO;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using DAL.Interfacies.Repository;
using DAL.Interfacies.Helper;
using DAL.Interfacies.DTO;
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

        public UserEntity GetById(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            return userRepository.GetById(id)?.ToBllUser();
        }

        public UserEntity GetOneByPredicate(Expression<Func<UserEntity, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<UserEntity, DalUser>(Expression.Parameter(typeof(DalUser), predicates.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<DalUser, bool>>(visitor.Visit(predicates.Body), visitor.NewParameterExp);
            return userRepository.GetOneByPredicate(exp2).ToBllUser();
        }

        public IEnumerable<UserEntity> GetAllByPredicate(Expression<Func<UserEntity, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<UserEntity, DalUser>(Expression.Parameter(typeof(DalUser), predicates.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<DalUser, bool>>(visitor.Visit(predicates.Body), visitor.NewParameterExp);
            return userRepository.GetAllByPredicate(exp2).Select(user => user.ToBllUser()).ToList();
        }

        public UserEntity GetUserEntityByNickname(string nickname) => userRepository.GetByNickname(nickname)?.ToBllUser();
        public UserEntity GetUserEntityByEmail(string email) => userRepository.GetByEmail(email)?.ToBllUser();
        #endregion

        public void AddRoleToUser(string nickname, string roleName)
        {
            /*if (nickname != null && nickname != String.Empty)
                throw new ArgumentException(nameof(nickname));

            if (roleName != null && roleName != String.Empty)
                throw new ArgumentException(nameof(roleName));

            userRepository.AddRoleToUser(nickname, roleName);
            unitOfWork.Commit();*/

            throw new NotImplementedException();
        }

        public IEnumerable<RoleEntity> GetRolesOfUser(int userId)
        {
            if (userId < 0)
                throw new ArgumentOutOfRangeException(nameof(userId));

            return roleRepository.GetRolesOfUser(userId).Select(r => r.ToBllRole());
        }

        public void ChangeAvatar(string nickname, HttpPostedFileBase file)
        {
            if (nickname == null)
                throw new ArgumentNullException(nameof(nickname));

            if (file == null)
                throw new ArgumentNullException(nameof(file));

            if (file.ContentLength < 0) ;
            // throw an exception

            var user = userRepository.GetByNickname(nickname);

            // John Skeet
            // As Darin says, you can read from the input stream - but I'd avoid relying on all the data being available in a single go. If you're using .NET 4 this is simple:

            // MemoryStream target = new MemoryStream();
            // model.File.InputStream.CopyTo(target);
            // byte[] data = target.ToArray();
            // It's easy enough to write the equivalent of CopyTo in .NET 3.5 if you want. The important part is that you read from HttpPostedFileBase.InputStream.

            // For efficient purposes you could check whether the stream returned is already a MemoryStream:


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
