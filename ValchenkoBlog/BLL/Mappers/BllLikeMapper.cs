using BLL.Interfacies.Entities;
using DAL.Interfacies.DTO;

namespace BLL.Mappers
{
    public static class BllLikeMapper
    {
        public static DalLike ToDalLike(this LikeEntity bllLike)
        {
            if (bllLike == null)
                return null;

            return new DalLike
            {
                Id = bllLike.Id,
                //PostId = bllLike.PostId,
                //UserId = bllLike.UserId
            };
        }

        public static LikeEntity ToBllLike(this DalLike dalLike)
        {
            if (dalLike == null)
                return null;

            return new LikeEntity
            {
                Id = dalLike.Id,
                //PostId = dalLike.PostId,
                //UserId = dalLike.UserId
            };
        }
    }
}
