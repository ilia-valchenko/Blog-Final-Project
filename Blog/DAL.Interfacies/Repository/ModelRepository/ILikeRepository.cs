using DAL.Interfacies.DTO;
using System.Collections.Generic;

namespace DAL.Interfacies.Repository.ModelRepository
{
    public interface ILikeRepository : IRepository<DalLike>
    {
        /// <summary>
        /// This method returns all DAL likes which user made.
        /// </summary>
        /// <param name="userId">Id of the user.</param>
        /// <returns>Returns collection of DAL likes.</returns>
        IEnumerable<DalLike> GetDalLikesByUserId(int userId);
        /// <summary>
        /// This method returns DAL likes of the post.
        /// </summary>
        /// <param name="postId">Id of the post.</param>
        /// <returns>Returns collection of DAL likes.</returns>
        IEnumerable<DalLike> GetDalLikesByPostId(int postId);
        /// <summary>
        /// This method finds like if user liked this post.
        /// </summary>
        /// <param name="userId">Id of the user.</param>
        /// <param name="postId">Id of the post.</param>
        /// <returns>Returns DAL like.</returns>
        DalLike GetDalLikeByPostIdAndUserId(int userId, int postId);
        /// <summary>
        /// This method removes all likes from a post.
        /// </summary>
        /// <param name="postId">Id of the post.</param>
        void DeleteLikesFromPost(int postId);
    }
}
