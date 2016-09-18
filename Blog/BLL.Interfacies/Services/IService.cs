using System.Collections.Generic;

namespace BLL.Interfacies.Services
{
    /// <summary>
    /// This interface provides basic CRUD and GET operations for services.
    /// </summary>
    /// <typeparam name="TEntity">BLL entity.</typeparam>
    public interface IService<TEntity>
    {
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();
    }
}
