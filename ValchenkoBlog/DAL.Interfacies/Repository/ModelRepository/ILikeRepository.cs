using DAL.Interfacies.DTO;
using System.Collections.Generic;

namespace DAL.Interfacies.Repository.ModelRepository
{
    public interface ILikeRepository : IRepository<DalLike>
    {
        IEnumerable<DalLike> GetDalLikesByUserId(int userId);
        IEnumerable<DalLike> GetDalLikesByPostId(int postId);
        DalLike GetDalLikeByPostIdAndUserId(int userId, int postId);
    }
}
