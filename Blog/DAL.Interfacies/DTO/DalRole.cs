namespace DAL.Interfacies.DTO
{
    /// <summary>
    /// This class represents a role on Data Access Layer.
    /// </summary>
    public class DalRole : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

