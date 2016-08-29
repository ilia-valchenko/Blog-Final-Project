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
using DAL.Interfacies.Repository;

namespace DAL.Concrete.ModelRepository
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(DbContext context)
        {
            this.context = context;
        }

        public void Create(DalUser entity)
        {
            if (entity == null)
                return;

            Role role = context.Set<Role>().FirstOrDefault(r => r.Name == "user");
            var user = entity.ToOrmUser();
            user.Roles.Add(role);
            context.Set<User>().Add(user);

            //unitOfWork.Context.Set<User>().Add(user);
            //unitOfWork.Commit();
        }

        public void Delete(DalUser entity)
        {
            var user = context.Set<User>().SingleOrDefault(u => u.UserId == entity.Id);
            if (user != default(User))
                context.Set<User>().Remove(user);

            //unitOfWork.Context.Set<User>().Remove(user);
            //unitOfWork.Commit();
        }

        public void Update(DalUser entity)
        {
            if (entity == null)
                return;

            var user = context.Set<User>().SingleOrDefault(u => u.UserId == entity.Id);

            if(user == default(User))
            {
                user = entity.ToOrmUser();
                context.Set<User>().Add(user);
                return;
            }

            user.Nickname = entity.Nickname;
            user.Password = entity.Password;
            user.Avatar = entity.Avatar;

            context.Entry(user).State = EntityState.Modified;
        }

        public DalUser GetById(int key) => context.Set<User>().FirstOrDefault(user => user.UserId == key)?.ToDalUser();

        public IEnumerable<DalUser> GetAll() => context.Set<User>().ToList().Select(u => u.ToDalUser());

        public DalUser GetOneByPredicate(Expression<Func<DalUser, bool>> predicate) => GetAllByPredicate(predicate).FirstOrDefault();

        public IEnumerable<DalUser> GetAllByPredicate(Expression<Func<DalUser, bool>> predicate)
        {
            var visitor = new PredicateExpressionVisitor<DalUser, User>(Expression.Parameter(typeof(User), predicate.Parameters[0].Name));
            var express = Expression.Lambda<Func<User, bool>>(visitor.Visit(predicate.Body), visitor.NewParameterExp);
            var final = context.Set<User>().Where(express).ToList();
            return final.Select(user => user.ToDalUser());
        }

        public DalUser GetByNickname(string nickname) => context.Set<User>().FirstOrDefault(u => u.Nickname == nickname)?.ToDalUser();

        //var ormUser = unitOfWork.Context.Set<User>().FirstOrDefault(user => user.Nickname == nickname);
        //return ormUser?.ToDalUser();

        public void AddRoleToUser(int userId, string roleName)
        {
            var user = context.Set<User>().FirstOrDefault(u => u.UserId == userId);

            if (user != null)
            {
                var role = context.Set<Role>().FirstOrDefault(r => r.Name == roleName);

                if (role != null)
                    user.Roles.Add(role);
            }
        }


        //private readonly IUnitOfWork unitOfWork;
        private DbContext context;
    }
}
