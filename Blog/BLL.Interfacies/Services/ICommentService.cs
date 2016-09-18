using BLL.Interfacies.Entities;
using System.Collections.Generic;

namespace BLL.Interfacies.Services
{
    public interface ICommentService : IService<CommentEntity>
    {
        /// <summary>
        /// This method returns collection of comment by post id.
        /// </summary>
        /// <param name="postId">Id of the post.</param>
        /// <returns>Returns collection of BLL comments.</returns>
        IEnumerable<CommentEntity> GetCommentsByPostId(int postId);
    }
}
