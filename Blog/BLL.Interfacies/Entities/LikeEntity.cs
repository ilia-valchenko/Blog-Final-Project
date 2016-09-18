namespace BLL.Interfacies.Entities
{
    /// <summary>
    /// This class represents a like on Business Logic Layer.
    /// </summary>
    public class LikeEntity
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
    }
}
