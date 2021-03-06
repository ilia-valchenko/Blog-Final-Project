﻿using System;
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

        public static TagEntity ToBllTag(this TagViewModel mvcTag)
        {
            if (mvcTag == null)
                throw new ArgumentNullException(nameof(mvcTag));

            return new TagEntity
            {
                Id = mvcTag.Id,
                Name = mvcTag.Name
            };
        }

        public static EditTagViewModel ToMvcEditTag(this TagEntity bllTag)
        {
            if (bllTag == null)
                return null;

            return new EditTagViewModel
            {
                Id = bllTag.Id,
                Name = bllTag.Name
            };
        }

        public static TagEntity ToBllTag(this EditTagViewModel mvcTag)
        {
            if (mvcTag == null)
                throw new ArgumentNullException(nameof(mvcTag));

            return new TagEntity
            {
                Id = mvcTag.Id,
                Name = mvcTag.Name
            };
        }
    }
}