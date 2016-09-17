using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using DAL.Interfacies.Repository;
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
        public RoleEntity GetById(int id) => roleRepository.GetById(id)?.ToBllRole(); 
        #endregion

        public IEnumerable<RoleEntity> GetRolesOfUser(int userId) => roleRepository.GetRolesOfUser(userId).Select(r => r.ToBllRole());

        private readonly IUnitOfWork unitOfWork;
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;
    }
}
