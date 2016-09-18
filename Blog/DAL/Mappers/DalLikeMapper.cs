using System;
using DAL.Interfacies.DTO;
using ORM.Models;

namespace DAL.Mappers
{
    /// <summary>
    /// This static class provides extension methods for DAL like entity.
    /// </summary>
    public static class DalLikeMapper
    {
        /// <summary>
        /// This method maps ORM like to DAL like.
        /// </summary>
        /// <param name="ormLike">ORM like.</param>
        /// <returns>DAL like.</returns>
        public static DalLike ToDalLike(this Like ormLike)
        {
            if (ormLike == null)
                return null;

            return new DalLike
            {
                Id = ormLike.LikeId,
                PostId = ormLike.Post.PostId,
                UserId = ormLike.User.UserId
            };
        }

        /// <summary>
        /// This method maps DAL like to ORM like.
        /// </summary>
        /// <param name="dalLike">DAL like.</param>
        /// <returns>ORM like.</returns>
        public static Like ToOrmLike(this DalLike dalLike)
        {
            if (dalLike == null)
                throw new ArgumentNullException(nameof(dalLike));

            return new Like();
        }
    }
}
