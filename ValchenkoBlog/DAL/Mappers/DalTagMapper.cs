using DAL.Interfacies.DTO;
using ORM.Models;

namespace DAL.Mappers
{
    public static class DalTagMapper
    {
        public static DalTag ToDalTag(this Tag ormTag)
        {
            if (ormTag == null)
                return null;

            return new DalTag
            {
                Id = ormTag.TagId,
                Name = ormTag.Name
            };
        }

        public static Tag ToOrmTag(this DalTag dalTag)
        {
            if (dalTag == null)
                return null;

            return new Tag
            {
                // without TagId
                Name = dalTag.Name
            };
        }
    }
}
