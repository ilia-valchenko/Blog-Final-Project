using BLL.Interfacies.Entities;

namespace BLL.Interfacies.Services
{
    public interface ITagService : IService<TagEntity>
    {
        /// <summary>
        /// This method finds BLL tag by given name.
        /// </summary>
        /// <param name="name">Name of searching tag.</param>
        /// <returns>Returns BLL tag with given name.</returns>
        TagEntity GetTagEntityByName(string name);
    }
}
