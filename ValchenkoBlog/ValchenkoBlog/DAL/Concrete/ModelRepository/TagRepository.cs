using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Interfacies.DTO;
using DAL.Interfacies.Repository.ModelRepository;
using ORM.Models;
using DAL.Mappers;
using System.Data.Entity;

namespace DAL.Concrete.ModelRepository
{
    public class TagRepository : ITagRepository
    {
        public TagRepository(DbContext context)
        {
            this.context = context;
        }

        #region CRUD operations
        public void Create(DalTag entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var tag = entity.ToOrmTag();
            context.Set<Tag>().Add(tag);
        }

        public void Update(DalTag entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var tag = context.Set<Tag>().SingleOrDefault(t => t.TagId == entity.Id);

            if (tag != null)
                tag.Name = entity.Name;
        }

        public void Delete(DalTag entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var tag = context.Set<Tag>().SingleOrDefault(t => t.TagId == entity.Id);

            if (tag != null)
                context.Set<Tag>().Remove(tag);
        }
        #endregion

        #region Get operations
        public DalTag GetById(int key) => context.Set<Tag>().FirstOrDefault(t => t.TagId == key)?.ToDalTag();
        public DalTag GetTagByName(string name) => context.Set<Tag>().FirstOrDefault(tag => tag.Name == name)?.ToDalTag();
        public IEnumerable<DalTag> GetAll() => context.Set<Tag>().ToList().Select(t => t.ToDalTag());
        public IEnumerable<DalTag> GetTagsByPostId(int postId) => context.Set<Post>().FirstOrDefault(post => post.PostId == postId)?.Tags.Select(tag => tag.ToDalTag());
        #endregion

        private readonly DbContext context;
    }
}
