using DAL.Interfacies.DTO;
using System.Collections.Generic;

namespace DAL.Interfacies.Repository.ModelRepository
{
    public interface ICommentRepository : IRepository<DalComment>
    {
        IEnumerable<DalComment> GetDalCommentsByUserId(int userId);
        IEnumerable<DalComment> GetDalCommentsByPostId(int postId);
        void DeleteCommentsFromPost(int postId);
    }
}
