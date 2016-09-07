using System;
using System.Collections.Generic;
using BLL.Interfacies.Entities;
using MvcPL.Models.Tag;

namespace MvcPL.Infrastructure.Mappers
{
    public static class MvcTagMapper
    {
        public static TagEntity ToBllTag(this CreateTagViewModel mvcTag)
        {
            if (mvcTag == null)
                throw new ArgumentNullException(nameof(mvcTag));

            return new TagEntity
            { 
                Name = mvcTag.Name,
            };
        }

        public static TagViewModel ToMvcTag(this TagEntity bllTag)
        {
            if (bllTag == null)
                return null;

            return new TagViewModel
            {
                Id = bllTag.Id,
                Name = bllTag.Name
            };
        }
    }
}