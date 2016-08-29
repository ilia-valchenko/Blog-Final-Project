using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interfacies.DTO;
using DAL.Interfacies.Repository.ModelRepository;
using ORM.Models;
using DAL.Mappers;
using DAL.Interfacies.Helper;
using System.Data.Entity;

namespace DAL.Concrete.ModelRepository
{
    public class RoleRepository : IRoleRepository
    {
        public RoleRepository(DbContext context)
        {
            //unitOfWork = uow;
            this.context = context;
        }

        public void Create(DalRole entity)
        {
            if (entity == null)
                return;

            var role = entity.ToOrmRole();
            context.Set<Role>().Add(role);
            //unitOfWork.Commit();
        }

        public void Delete(DalRole entity)
        {
            var role = context.Set<Role>().SingleOrDefault(r => r.RoleId == entity.Id);

            if(role != default(Role))
                context.Set<Role>().Remove(role);
            //unitOfWork.Commit();
        }

        public void Update(DalRole entity)
        {
            if (entity == null)
                return;

            var role = context.Set<Role>().SingleOrDefault(r => r.RoleId == entity.Id);

            if(role == default(Role))
            {
                role = entity.ToOrmRole();
                context.Set<Role>().Add(role);
                return;
            }

            role.Name = entity.Name;
            context.Entry(role).State = EntityState.Modified;

            //unitOfWork.Context.Set<Role>().AddOrUpdate(entity.ToOrmRole());
            //unitOfWork.Commit();
        }

        public DalRole GetById(int key) => context.Set<Role>().FirstOrDefault(r => r.RoleId == key)?.ToDalRole();

        public IEnumerable<DalRole> GetAll() => context.Set<Role>().ToList().Select(r => r.ToDalRole());

        public DalRole GetOneByPredicate(Expression<Func<DalRole, bool>> predicate) => GetAllByPredicate(predicate).FirstOrDefault();

        public IEnumerable<DalRole> GetAllByPredicate(Expression<Func<DalRole, bool>> predicate)
        {
            var visitor = new PredicateExpressionVisitor<DalRole, Role>(Expression.Parameter(typeof(Role), predicate.Parameters[0].Name));
            var express = Expression.Lambda<Func<Role, bool>>(visitor.Visit(predicate.Body), visitor.NewParameterExp);
            var final = context.Set<Role>().Where(express).ToList();
            return final.Select(role => role.ToDalRole());
        }

        public DalRole GetRoleByName(string name) => context.Set<Role>().FirstOrDefault(role => role.Name == name)?.ToDalRole();

        public IEnumerable<DalRole> GetRolesOfUser(int userId) => context.Set<User>().FirstOrDefault(u => u.UserId == userId)?.Roles.Select(r => r.ToDalRole()).ToList();

        /*public void AddUserToRole(int userId, string roleName)
        {
            var user = context.Set<User>().FirstOrDefault(u => u.UserId == userId);

            if(user != null)
            {
                var role = context.Set<Role>().FirstOrDefault(r => r.Name == roleName);

                if (role != null)
                    role.Users.Add(user);
            }
        }*/

        //private readonly UnitOfWork unitOfWork;
        private DbContext context;
    }
}
