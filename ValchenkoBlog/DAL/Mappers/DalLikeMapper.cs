using DAL.Interfacies.DTO;
using ORM.Models;

namespace DAL.Mappers
{
    public static class DalLikeMapper
    {
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

        public static Like ToOrmLike(this DalLike dalLike)
        {
            if (dalLike == null)
                return null;

            return new Like();
        }
    }
}
