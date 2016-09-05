using BLL.Interfacies.Entities;
using System.Collections.Generic;

namespace BLL.Interfacies.Services
{
    public interface IPostService : IService<PostEntity>
    {
        void Like(LikeEntity likeEntity);
        void AddComment(CommentEntity commentEntity);
        void RemoveComment(CommentEntity commentEntity);

        void AddTagsToPost(int postId, string[] tags);

        IEnumerable<PostEntity> GetPostsByTagName(string tagName);
        // Should it situated here or in the like service?
        IEnumerable<LikeEntity> GetLikesByPostId(int postId);
        IEnumerable<CommentEntity> GetCommentsByPostId(int postId);
    }
}
