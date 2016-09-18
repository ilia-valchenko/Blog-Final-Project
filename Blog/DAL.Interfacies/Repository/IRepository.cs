using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DAL.Interfacies.DTO;

namespace DAL.Interfacies.Repository
{
    /// <summary>
    /// This interface provides basic CRUD and GET operations for repositories.
    /// </summary>
    /// <typeparam name="TEntity">DAL entity.</typeparam>
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        // Create and Update operations should return entity or id of this entity.

        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        TEntity GetById(int key);
        IEnumerable<TEntity> GetAll();
    }
}
