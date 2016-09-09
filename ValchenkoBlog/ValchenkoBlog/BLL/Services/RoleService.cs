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
    public class RoleService : IRoleService
    {
        public RoleService(IUnitOfWork unitOfWork,
                           IUserRepository userRepository,
                           IRoleRepository roleRepository)
        {
            this.unitOfWork = unitOfWork;
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
        }

        #region CRUD operations
        public void Create(RoleEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            roleRepository.Create(entity.ToDalRole());
            unitOfWork.Commit();
        }
        public void Update(RoleEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            roleRepository.Update(entity.ToDalRole());
            unitOfWork.Commit();
        }
        public void Delete(RoleEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            roleRepository.Delete(entity.ToDalRole());
            unitOfWork.Commit();
        }
        #endregion

        #region Get operations
        public IEnumerable<RoleEntity> GetAll() => roleRepository.GetAll().Select(r => r.ToBllRole());
        public RoleEntity GetById(int? id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            if (id == null)
                return null;

            return roleRepository.GetById(id)?.ToBllRole();
        }
        public RoleEntity GetOneByPredicate(Expression<Func<RoleEntity, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<RoleEntity, DalRole>(Expression.Parameter(typeof(DalRole), predicates.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<DalRole, bool>>(visitor.Visit(predicates.Body), visitor.NewParameterExp);
            return roleRepository.GetOneByPredicate(exp2).ToBllRole();
        }
        public IEnumerable<RoleEntity> GetAllByPredicate(Expression<Func<RoleEntity, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<RoleEntity, DalRole>(Expression.Parameter(typeof(DalRole), predicates.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<DalRole, bool>>(visitor.Visit(predicates.Body), visitor.NewParameterExp);
            return roleRepository.GetAllByPredicate(exp2).Select(role => role.ToBllRole()).ToList();
        } 
        #endregion

        public IEnumerable<RoleEntity> GetRolesOfUser(int userId)
        {
            if (userId < 0)
                throw new ArgumentOutOfRangeException(nameof(userId));

            return roleRepository.GetRolesOfUser(userId).Select(r => r.ToBllRole());
        }

        public void AddUserToRole(int userId, string roleName)
        {
            // FIX IT
            throw new NotImplementedException();
        }

        private readonly IUnitOfWork unitOfWork;
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;
    }
}
