namespace DAL.Interfacies.DTO
{
    /// <summary>
    /// This class represents a tag on Data Access Layer.
    /// </summary>
    public class DalTag : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

