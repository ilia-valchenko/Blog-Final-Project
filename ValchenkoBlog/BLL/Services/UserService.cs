using System;
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
                           IRoleRepository roleRepository
                           /*IPostRepository postRepository, 
                           ICommentRepository commentRepository,
                           ILikeRepository likeRepository*/)
        {
            this.unitOfWork = unitOfWork;
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            /*this.postRepository = postRepository;
            this.commentRepository = commentRepository;
            this.likeRepository = likeRepository;*/
        }

        #region CRUD operations
        public int Create(UserEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            int idOfCreatedUser = userRepository.Create(entity.ToDalUser());
            unitOfWork.Commit();
            return idOfCreatedUser;
        }

        public int Update(UserEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            int idOfUpdatedUser = userRepository.Update(entity.ToDalUser());
            unitOfWork.Commit();
            return idOfUpdatedUser;
        }

        public void Delete(UserEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            userRepository.Delete(entity.ToDalUser());
            unitOfWork.Commit();
        } 
        #endregion

        public IEnumerable<UserEntity> GetAll() => userRepository.GetAll().Select(u => u.ToBllUser());

        public UserEntity GetById(int? id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            if (id == null)
                return null;

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

        public void AddRoleToUser(string nickname, string roleName)
        {
            if (nickname != null && nickname != String.Empty)
                throw new ArgumentException(nameof(nickname));

            if (roleName != null && roleName != String.Empty)
                throw new ArgumentException(nameof(roleName));

            userRepository.AddRoleToUser(nickname, roleName);
            unitOfWork.Commit();
        }

        public IEnumerable<RoleEntity> GetRolesOfUser(int userId)
        {
            if (userId < 0)
                throw new ArgumentOutOfRangeException(nameof(userId));

            return roleRepository.GetRolesOfUser(userId).Select(r => r.ToBllRole());
        }

        private readonly IUnitOfWork unitOfWork;
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;
        /*private readonly IPostRepository postRepository;
        private readonly ICommentRepository commentRepository;
        private readonly ILikeRepository likeRepository;*/
    }
}
