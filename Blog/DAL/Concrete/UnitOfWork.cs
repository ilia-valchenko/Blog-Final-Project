using System.Data.Entity;
using DAL.Interfacies.Repository;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace DAL.Concrete
{
    /// <summary>
    /// This class realize Unit of work pattern. Which allow to make save changes 
    /// after a whole transaction by using Commit method.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Context of the database.
        /// </summary>
        public DbContext Context { get; private set; }

        public UnitOfWork(DbContext context)
        {
            Context = context;
        }

        /// <summary>
        /// This method save changes which constitute a single transaction.
        /// </summary>
        public void Commit()
        {
            if (Context != null)
            {
                try
                {
                    Context.SaveChanges();
                }
                catch (DbEntityValidationException exc)
                {
                    // Write this in a log.
                    foreach (var eve in exc.EntityValidationErrors)
                    {
                        Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }

                    throw;
                }
            }
        }

        public void Dispose() => Context?.Dispose();
    }
}
