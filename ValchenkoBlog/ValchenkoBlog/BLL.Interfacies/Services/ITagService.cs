using BLL.Interfacies.Entities;
using System.Collections.Generic;

namespace BLL.Interfacies.Services
{
    public interface ITagService : IService<TagEntity>
    {
        TagEntity GetTagEntityByName(string name);
    }
}
