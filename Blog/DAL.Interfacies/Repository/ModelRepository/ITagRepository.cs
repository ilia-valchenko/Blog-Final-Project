using DAL.Interfacies.DTO;
using System.Collections.Generic;

namespace DAL.Interfacies.Repository.ModelRepository
{
    public interface ITagRepository : IRepository<DalTag>
    {
        /// <summary>
        /// This method finds DAL tag by given name.
        /// </summary>
        /// <param name="name">Tag's name.</param>
        /// <returns>Returns DAL tag.</returns>
        DalTag GetTagByName(string name);
        /// <summary>
        /// This method finds all tags of post by given post's id.
        /// </summary>
        /// <param name="postId">Id of the post.</param>
        /// <returns>Returns collection of DAL tags.</returns>
        IEnumerable<DalTag> GetTagsByPostId(int postId);
    }
}
