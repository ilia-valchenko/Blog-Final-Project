using DAL.Interfacies.DTO;
using System.Collections.Generic;

namespace DAL.Interfacies.Repository.ModelRepository
{
    public interface IPostRepository : IRepository<DalPost>
    {
        /// <summary>
        /// This method creates a post by using DAL post and collection of DAL tags.
        /// </summary>
        /// <param name="entity">A simple DAL post.</param>
        /// <param name="tags">Collection of DAL tags which belong to the post.</param>
        void Create(DalPost entity, IEnumerable<DalTag> tags);
        /// <summary>
        /// This method updates a post by using DAL post and collection od DAL tags.
        /// </summary>
        /// <param name="entity">A simple DAL post.</param>
        /// <param name="tags">Collecion of DAL tags.</param>
        void Update(DalPost entity, IEnumerable<DalTag> tags);
        /// <summary>
        /// This method returns all DAL posts which were posted by this user.
        /// </summary>
        /// <param name="userId">User's id.</param>
        /// <returns>Returns collection of DAL posts.</returns>
        IEnumerable<DalPost> GetDalPostsByUserId(int userId);
        /// <summary>
        /// This method returns all DAL posts which contain the give hashtag.
        /// </summary>
        /// <param name="tagName">Name of hashtag.</param>
        /// <returns>Returns collection of DAL posts.</returns>
        IEnumerable<DalPost> GetDalPostsByTagName(string tagName);
        /// <summary>
        /// This method gets DAL posts for one page by using give parameters such as size of page and number of page.
        /// </summary>
        /// <param name="pageSize">The number of items on one page.</param>
        /// <param name="pageNumber">Number of page.</param>
        /// <returns>Returns collection of DAL posts for one page.</returns>
        IEnumerable<DalPost> GetPostsForPage(int pageSize, int pageNumber);
        /// <summary>
        /// This method adds tags to a post.
        /// </summary>
        /// <param name="postId">Id of post.</param>
        /// <param name="tags">Array of names of tags.</param>
        void AddTagsToPost(int postId, string[] tags);
        /// <summary>
        /// This method returns number of posts in the database.
        /// </summary>
        /// <returns>Returns number of posts in the database.</returns>
        int Count();
    }
}
