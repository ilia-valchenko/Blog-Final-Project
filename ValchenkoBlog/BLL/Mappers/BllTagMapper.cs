using System;
using BLL.Interfacies.Entities;
using DAL.Interfacies.DTO;

namespace BLL.Mappers
{
    public static class BllTagMapper
    {
        public static DalTag ToDalTag(this TagEntity bllTag)
        {
            if (bllTag == null)
                throw new ArgumentNullException(nameof(bllTag));

            return new DalTag
            {
                Id = bllTag.Id,
                Name = bllTag.Name
            };
        }

        public static TagEntity ToBllTag(this DalTag dalTag)
        {
            if (dalTag == null)
                return null;

            return new TagEntity
            {
                Id = dalTag.Id,
                Name = dalTag.Name
            };
        }
    }
}
