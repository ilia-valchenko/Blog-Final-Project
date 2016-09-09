using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BLL.Interfacies.Services
{
    public interface IService<TEntity>
    {
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        TEntity GetById(int? id);
        IEnumerable<TEntity> GetAll();
        TEntity GetOneByPredicate(Expression<Func<TEntity, bool>> predicates);
        IEnumerable<TEntity> GetAllByPredicate(Expression<Func<TEntity, bool>> predicates);
    }
}
