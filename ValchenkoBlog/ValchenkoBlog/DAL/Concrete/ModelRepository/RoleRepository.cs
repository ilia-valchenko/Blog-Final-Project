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
    public class RoleRepository : IRoleRepository
    {
        public RoleRepository(DbContext context)
        {
            this.context = context;
        }

        #region CRUD operations
        public void Create(DalRole entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var role = entity.ToOrmRole();
            context.Set<Role>().Add(role);
        }

        public void Update(DalRole entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var role = context.Set<Role>().SingleOrDefault(r => r.RoleId == entity.Id);

            if(role != null)
                role.Name = entity.Name;
        }

        public void Delete(DalRole entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var role = context.Set<Role>().SingleOrDefault(r => r.RoleId == entity.Id);

            if (role != null)
                context.Set<Role>().Remove(role);
        }
        #endregion

        #region Get operations
        public DalRole GetById(int key) => context.Set<Role>().FirstOrDefault(r => r.RoleId == key)?.ToDalRole();
        public DalRole GetRoleByName(string name) => context.Set<Role>().FirstOrDefault(role => role.Name == name)?.ToDalRole();
        public IEnumerable<DalRole> GetAll() => context.Set<Role>().ToList().Select(r => r.ToDalRole());
        public IEnumerable<DalRole> GetRolesOfUser(int userId) => context.Set<User>().FirstOrDefault(u => u.UserId == userId)?.Roles.Select(r => r.ToDalRole()).ToList(); 
        #endregion

        private readonly DbContext context;
    }
}
