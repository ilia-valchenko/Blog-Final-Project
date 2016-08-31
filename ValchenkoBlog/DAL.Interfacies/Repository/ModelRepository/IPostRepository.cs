using DAL.Interfacies.DTO;
using System.Collections.Generic;

namespace DAL.Interfacies.Repository.ModelRepository
{
    public interface IPostRepository : IRepository<DalPost>
    {
        IEnumerable<DalPost> GetDalPostsByUserId(int userId);
        IEnumerable<DalPost> GetDalPostsByTagName(string tagName);
        void AddTagsToPost(int postId, string[] tags);
    }
}
