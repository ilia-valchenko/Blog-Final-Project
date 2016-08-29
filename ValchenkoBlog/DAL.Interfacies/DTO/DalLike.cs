namespace DAL.Interfacies.DTO
{
    public class DalLike : IEntity
    {
        public int Id { get; set; }

        //public virtual DalPost Post { get; set; }
        public int PostId;
        //public virtual DalUser User { get; set; }
        public int UserId;
    }
}
