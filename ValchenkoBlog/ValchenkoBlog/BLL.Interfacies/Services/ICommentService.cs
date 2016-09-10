using BLL.Interfacies.Entities;
using System.Collections.Generic;

namespace BLL.Interfacies.Services
{
    public interface ICommentService : IService<CommentEntity>
    {
        IEnumerable<CommentEntity> GetCommentsByPostId(int postId);
    }
}
