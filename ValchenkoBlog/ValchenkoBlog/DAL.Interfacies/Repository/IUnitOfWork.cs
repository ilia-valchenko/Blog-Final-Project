using System;
using DAL.Interfacies.Repository.ModelRepository;

namespace DAL.Interfacies.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        /*ICommentRepository CommentRepository { get; }
        IPostRepository PostRepository { get; }
        IRoleRepository RoleRepository { get; }
        ITagRepository TagRepository { get; }
        IUserRepository UserRepository { get; }*/

        void Commit();
        //Rollback
        //Dispose
    }
}