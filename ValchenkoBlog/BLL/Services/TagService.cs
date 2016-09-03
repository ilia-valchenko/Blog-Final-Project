using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using DAL.Interfacies.Repository;
using DAL.Interfacies.Helper;
using DAL.Interfacies.DTO;
using DAL.Interfacies.Repository.ModelRepository;
using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;
using BLL.Mappers;

namespace BLL.Services
{
    public class TagService : ITagService
    {
        public TagService(IUnitOfWork unitOfWork,
                           IPostRepository postRepository,
                           ITagRepository tagRepository)
        {
            this.unitOfWork = unitOfWork;
            this.tagRepository = tagRepository;
            this.postRepository = postRepository;
        }

        public TagEntity GetTagByName(string name)
        {
            throw new NotImplementedException();
        }

        public int Create(TagEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            int idOfCreatedTag = tagRepository.Create(entity.ToDalTag());
            unitOfWork.Commit();
            return idOfCreatedTag;
        }

        public int Update(TagEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            int idOfUpdatedTag = tagRepository.Update(entity.ToDalTag());
            unitOfWork.Commit();
            return idOfUpdatedTag;
        }

        public void Delete(TagEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            tagRepository.Delete(entity.ToDalTag());
            unitOfWork.Commit();
        }

        public TagEntity GetById(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            return tagRepository.GetById(id)?.ToBllTag();
        }

        public IEnumerable<TagEntity> GetAll() => tagRepository.GetAll().Select(t => t.ToBllTag());

        public TagEntity GetOneByPredicate(Expression<Func<TagEntity, bool>> predicates)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TagEntity> GetAllByPredicate(Expression<Func<TagEntity, bool>> predicates)
        {
            throw new NotImplementedException();
        }

        /*public IEnumerable<TagEntity> GetTagsOfPost(int postId)
        {
            if (postId < 0)
                throw new ArgumentOutOfRangeException(nameof(postId));

            return tagRepository.GetTagsOfPost(postId).Select(tag => tag.ToBllTag());
        }*/

        private readonly IUnitOfWork unitOfWork;
        private readonly ITagRepository tagRepository;
        private readonly IPostRepository postRepository;
    }
}
