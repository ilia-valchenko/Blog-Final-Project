using System;
using DAL.Interfacies.Repository.ModelRepository;

namespace DAL.Interfacies.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        //Rollback
        //Dispose
    }
}