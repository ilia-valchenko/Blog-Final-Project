using BLL.Interfacies.Entities;
using System.Collections.Generic;

namespace BLL.Interfacies.Services
{
    interface ITagService : IService<TagEntity>
    {
        TagEntity GetTagByName(string name);
    }
}
