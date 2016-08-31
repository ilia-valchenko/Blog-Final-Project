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
            // if null

            return new TagEntity
            { 
                // ?? Id
                Name = mvcTag.Name,
            };
        }

        public static TagViewModel ToMvcTag(this TagEntity bllTag)
        {
            return new TagViewModel
            {
                Id = bllTag.Id,
                Name = bllTag.Name
            };
        }
    }
}