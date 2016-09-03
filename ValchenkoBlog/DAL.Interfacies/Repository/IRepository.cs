using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DAL.Interfacies.DTO;

namespace DAL.Interfacies.Repository
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        int Create(TEntity entity);
        int Update(TEntity entity);
        void Delete(TEntity entity);
        TEntity GetById(int? key);
        IEnumerable<TEntity> GetAll();
        TEntity GetOneByPredicate(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetAllByPredicate(Expression<Func<TEntity, bool>> predicate); 
    }
}
