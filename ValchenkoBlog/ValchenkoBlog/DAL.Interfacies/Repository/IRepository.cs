using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DAL.Interfacies.DTO;

namespace DAL.Interfacies.Repository
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        TEntity GetById(int key);
        IEnumerable<TEntity> GetAll(); 
    }
}
