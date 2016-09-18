using System;
using DAL.Interfacies.DTO;
using ORM.Models;

namespace DAL.Mappers
{
    /// <summary>
    /// This static class provides extension methods for DAL tag entity.
    /// </summary>
    public static class DalTagMapper
    {
        /// <summary>
        /// This method maps ORM tag to DAL tag.
        /// </summary>
        /// <param name="ormTag">ORM tag.</param>
        /// <returns>DAL tag.</returns>
        public static DalTag ToDalTag(this Tag ormTag)
        {
            if (ormTag == null)
                return null;

            return new DalTag
            {
                Id = ormTag.TagId,
                Name = ormTag.Name
            };
        }

        /// <summary>
        /// This method maps DAL tag to ORM tag.
        /// </summary>
        /// <param name="dalTag">DAL tag.</param>
        /// <returns>ORM tag.</returns>
        public static Tag ToOrmTag(this DalTag dalTag)
        {
            if (dalTag == null)
                throw new ArgumentNullException(nameof(dalTag));

            return new Tag
            {
                Name = dalTag.Name
            };
        }
    }
}
