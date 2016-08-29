using DAL.Interfacies.DTO;

namespace DAL.Interfacies.Repository.ModelRepository
{
    public interface ITagRepository : IRepository<DalTag>
    {
        DalTag GetTagByName(string name);
    }
}
