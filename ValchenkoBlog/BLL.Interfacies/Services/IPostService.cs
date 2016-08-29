using BLL.Interfacies.Entities;
using System.Collections.Generic;

namespace BLL.Interfacies.Services
{
    public interface IPostService : IService<PostEntity>
    {
        void AddLike(LikeEntity likeEntity);
        void RemoveLike(LikeEntity likeEntity);
        // Is it necessary?
        void AddComment(CommentEntity commentEntity);
        void RemoveComment(CommentEntity commentEntity);
    }
}
