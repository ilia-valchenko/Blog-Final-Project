using BLL.Interfacies.Entities;
using System.Collections.Generic;

namespace BLL.Interfacies.Services
{
    public interface IPostService : IService<PostEntity>
    {
        /// <summary>
        /// This method add like to the post or delete like if it already exist.
        /// </summary>
        /// <param name="likeEntity">BLL like.</param>
        /// <returns>Returns true if it was like and false if it was dislike.</returns>
        bool Like(LikeEntity likeEntity);
        /// <summary>
        /// This method adds tags to the post.
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="tags"></param>
        void AddTagsToPost(int postId, string[] tags);
        /// <summary>
        /// This method finds DAL posts by tag name. 
        /// </summary>
        /// <param name="tagName">Name of tag.</param>
        /// <returns>Returns collection of DAL posts.</returns>
        IEnumerable<PostEntity> GetPostsByTagName(string tagName);
        /// <summary>
        /// This method returns collection of DAL likes which belong to the post.
        /// </summary>
        /// <param name="postId">Id of the post.</param>
        /// <returns>Returns collection of DAL posts.</returns>
        IEnumerable<LikeEntity> GetLikesByPostId(int postId);
        /// <summary>
        /// This method returns collection of DAL comments which belong to the post.
        /// </summary>
        /// <param name="postId">Id of the post.</param>
        /// <returns>Returns collection of DAL comments.</returns>
        IEnumerable<CommentEntity> GetCommentsByPostId(int postId);
        /// <summary>
        /// This method gets BLL posts for one page by using give parameters such as size of page and number of page.
        /// </summary>
        /// <param name="pageSize">The number of items on one page.</param>
        /// <param name="pageNumber">Number of page.</param>
        /// <returns>Returns collection of BLL posts for one page.</returns>
        IEnumerable<PostEntity> GetPostsForPage(int pageSize, int pageNumber);
        /// <summary>
        /// This method determines is post was liked.
        /// </summary>
        /// <param name="nickname">Nickname of the user.</param>
        /// <param name="likes">Collection of likes.</param>
        /// <returns>Returns true is post was liked.</returns>
        bool IsLikedPost(string nickname, IEnumerable<LikeEntity> likes);
        /// <summary>
        /// This method returns number of posts in the database.
        /// </summary>
        /// <returns>Returns number of posts in the database.</returns>
        int Count();
    }
}
