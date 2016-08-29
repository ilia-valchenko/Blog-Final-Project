using System;
using System.Collections.Generic;

namespace BLL.Interfacies.Entities
{
    public class PostEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }

        public int UserId { get; set; }
    }
}
