using DAL.Interfacies.DTO;
using System.Collections.Generic;

namespace DAL.Interfacies.Repository.ModelRepository
{
    public interface ICommentRepository : IRepository<DalComment>
    {
        /// <summary>
        /// This method returns all DAL comments which was posted by the user.
        /// </summary>
        /// <param name="userId">Id of the user.</param>
        /// <returns></returns>
        IEnumerable<DalComment> GetDalCommentsByUserId(int userId);
        /// <summary>
        /// This method returns DAL comments of the post.
        /// </summary>
        /// <param name="postId">Id of the post.</param>
        /// <returns>Returns collection of DAL comments.</returns>
        IEnumerable<DalComment> GetDalCommentsByPostId(int postId);
        /// <summary>
        /// This method delete comments from the post.
        /// </summary>
        /// <param name="postId">Id of the post.</param>
        void DeleteCommentsFromPost(int postId);
    }
}
