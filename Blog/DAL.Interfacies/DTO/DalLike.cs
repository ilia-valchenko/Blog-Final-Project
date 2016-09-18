namespace DAL.Interfacies.DTO
{
    /// <summary>
    /// This class represents a like on Data Access Layer.
    /// </summary>
    public class DalLike : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
    }
}
