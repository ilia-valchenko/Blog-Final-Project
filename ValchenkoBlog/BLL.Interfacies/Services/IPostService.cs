using BLL.Interfacies.Entities;
using System.Collections.Generic;

namespace BLL.Interfacies.Services
{
    public interface IPostService : IService<PostEntity>
    {
        void Create(PostEntity entity, string[] tags);
        void AddLike(LikeEntity likeEntity);
        void RemoveLike(LikeEntity likeEntity);
        // Is it necessary?
        void AddComment(CommentEntity commentEntity);
        void RemoveComment(CommentEntity commentEntity);

        void AddTagsToPost(int postId, string[] tags);

        IEnumerable<PostEntity> GetPostsByTagName(string tagName);

        IEnumerable<LikeEntity> GetLikesByPostId(int postId);
        IEnumerable<CommentEntity> GetCommentsByPostId(int postId);
    }
}
