using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfacies.DTO
{
    public class DalRole : IEntity
    {
        // Add new ctor
        /*public DalRole()
        {
            Users = new HashSet<DalUser>();
        }*/

        public int Id { get; set; }
        public string Name { get; set; }

        // Add new 
        //public ICollection<DalUser> Users { get; set; }
    }
}

