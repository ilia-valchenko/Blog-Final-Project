using System.Collections.Generic;

namespace DAL.Interfacies.DTO
{
    /// <summary>
    /// This class represents a user on Data Access Layer.
    /// </summary>
    public class DalUser : IEntity
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte[] Avatar { get; set; }
    }
}
