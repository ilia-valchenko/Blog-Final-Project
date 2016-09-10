using DAL.Interfacies.DTO;
using System.Collections.Generic;

namespace DAL.Interfacies.Repository.ModelRepository
{
    public interface IPostRepository : IRepository<DalPost>
    {
        // Add new overloaded 'Create' method
        void Create(DalPost entity, IEnumerable<DalTag> tags);
        void Update(DalPost entity, IEnumerable<DalTag> tags);
        IEnumerable<DalPost> GetDalPostsByUserId(int userId);
        IEnumerable<DalPost> GetDalPostsByTagName(string tagName);
        IEnumerable<DalPost> GetPostsForPage(int pageSize, int pageNumber);
        void AddTagsToPost(int postId, string[] tags);
        int Count();
    }
}
