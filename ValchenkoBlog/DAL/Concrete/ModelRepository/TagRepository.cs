using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interfacies.DTO;
using DAL.Interfacies.Repository.ModelRepository;
using ORM.Models;
using DAL.Mappers;
using DAL.Interfacies.Helper;
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

            if (tag == default(Tag))
            {
                tag = entity.ToOrmTag();
                context.Set<Tag>().Add(tag);
            }

            tag.Name = entity.Name;
            context.Entry(tag).State = EntityState.Modified;
        }

        public void Delete(DalTag entity)
        {
            var tag = context.Set<Tag>().SingleOrDefault(t => t.TagId == entity.Id);

            if (tag != default(Tag))
                context.Set<Tag>().Remove(tag);
        }
        #endregion

        #region Get operations
        // NULLABLE
        public DalTag GetById(int? key) => context.Set<Tag>().FirstOrDefault(t => t.TagId == key)?.ToDalTag();

        public IEnumerable<DalTag> GetAll() => context.Set<Tag>().ToList().Select(t => t.ToDalTag());

        public DalTag GetOneByPredicate(Expression<Func<DalTag, bool>> predicate) => GetAllByPredicate(predicate).FirstOrDefault();

        public IEnumerable<DalTag> GetAllByPredicate(Expression<Func<DalTag, bool>> predicate)
        {
            var visitor = new PredicateExpressionVisitor<DalTag, Tag>(Expression.Parameter(typeof(Tag), predicate.Parameters[0].Name));
            var express = Expression.Lambda<Func<Tag, bool>>(visitor.Visit(predicate.Body), visitor.NewParameterExp);
            var final = context.Set<Tag>().Where(express).ToList();
            return final.Select(tag => tag.ToDalTag());
        }

        public DalTag GetTagByName(string name) => context.Set<Tag>().FirstOrDefault(tag => tag.Name == name)?.ToDalTag();

        public IEnumerable<DalTag> GetTagsByPostId(int postId) => context.Set<Post>().FirstOrDefault(post => post.PostId == postId)?.Tags.Select(tag => tag.ToDalTag());
        #endregion

        /*public void DeleteTagsFromPost(int postId)
        {
            if (postId < 0)
                throw new ArgumentOutOfRangeException(nameof(postId));

            var post = context.Set<Post>().FirstOrDefault(p => p.PostId == postId);

            if (post != null)
                foreach (var tag in post.Tags)
                    context.Set<Tag>().Remove(tag);
        }*/

        private readonly DbContext context;
    }
}
