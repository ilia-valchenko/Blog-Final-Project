namespace DAL.Interfacies.DTO
{
    public class DalLike : IEntity
    {
        public int Id { get; set; }

        // Add
        public int UserId { get; set; }
        public int PostId { get; set; }
        // Add new
        /*public DalPost Post { get; set; }
        public DalUser User { get; set; }*/
    }
}
