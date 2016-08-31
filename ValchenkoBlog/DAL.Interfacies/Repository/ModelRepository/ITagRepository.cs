using DAL.Interfacies.DTO;
using System.Collections.Generic;

namespace DAL.Interfacies.Repository.ModelRepository
{
    public interface ITagRepository : IRepository<DalTag>
    {
        DalTag GetTagByName(string name);
        IEnumerable<DalTag> GetTagsOfPost(int postId);
    }
}
