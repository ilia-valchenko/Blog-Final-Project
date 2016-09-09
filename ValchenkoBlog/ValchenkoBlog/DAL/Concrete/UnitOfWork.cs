using System.Data.Entity;
using DAL.Interfacies.Repository;
using DAL.Interfacies.Repository.ModelRepository;
using System.Data.Entity.Validation;
// Fix it. Write into a log.
using System.Diagnostics;

namespace DAL.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; private set; }
        
        /*public ICommentRepository CommentRepository { get; private set; }
        public IPostRepository PostRepository { get; private set; }
        public IRoleRepository RoleRepository { get; private set; }
        public ITagRepository TagRepository { get; private set; }
        public IUserRepository UserRepository { get; private set; }*/

        public UnitOfWork(DbContext context
                          /*ICommentRepository commentRepository,
                          IPostRepository postRepository,
                          IRoleRepository roleRepository,
                          ITagRepository tagRepository,
                          IUserRepository userRepository*/)
        {
            Context = context;
            /*CommentRepository = commentRepository;
            PostRepository = postRepository;
            RoleRepository = roleRepository;
            TagRepository = tagRepository;
            UserRepository = userRepository;*/
        }

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
                    // Write this into a log.
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

        /// ------------------------ OLD ---------------------------////

        //public DbContext Context { get; private set; }

        //public UnitOfWork(DbContext context)
        //{
        //    Context = context;
        //}

        //public void Commit()
        //{
        //    if(Context != null)
        //    {
        //        try
        //        {
        //            Context.SaveChanges();
        //        }
        //        catch(DbEntityValidationException exc)
        //        {
        //            // Write this into a log.
        //            foreach (var eve in exc.EntityValidationErrors)
        //            {
        //                Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
        //                    eve.Entry.Entity.GetType().Name, eve.Entry.State);
        //                foreach (var ve in eve.ValidationErrors)
        //                {
        //                    Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
        //                        ve.PropertyName, ve.ErrorMessage);
        //                }
        //            }
        //            throw;
        //        }
        //    }
        //}

        ///*
        //#region IDisposable Support
        //private bool disposedValue = false; // To detect redundant calls

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!disposedValue)
        //    {
        //        if (disposing)
        //        {
        //            // TODO: dispose managed state (managed objects).
        //        }

        //        // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
        //        // TODO: set large fields to null.

        //        disposedValue = true;
        //    }
        //}

        //// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        //// ~UnitOfWork() {
        ////   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        ////   Dispose(false);
        //// }

        //// This code added to correctly implement the disposable pattern.
        //public void Dispose()
        //{
        //    // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //    Dispose(true);
        //    // TODO: uncomment the following line if the finalizer is overridden above.
        //    // GC.SuppressFinalize(this);
        //}
        //#endregion
        //*/

        //public void Dispose() => Context?.Dispose();
    }
}
