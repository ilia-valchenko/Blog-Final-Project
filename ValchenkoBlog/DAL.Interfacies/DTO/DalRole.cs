using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfacies.DTO
{
    public class DalRole : IEntity
    {
        //public DalRole()
        //{
        //    Users = new HashSet<DalUser>();
        //}

        public int Id { get; set; }
        public string Name { get; set; }

        //public virtual ICollection<DalUser> Users { get; set; }
        //public int UserId { get; set; }
    }
}

