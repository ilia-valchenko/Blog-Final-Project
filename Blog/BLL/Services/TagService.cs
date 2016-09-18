using System;
using System.Linq;
using System.Collections.Generic;
using DAL.Interfacies.Repository;
using DAL.Interfacies.Repository.ModelRepository;
using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;
using BLL.Mappers;

namespace BLL.Services
{
    /// <summary>
    /// Realization of ITagService interface.
    /// </summary>
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

        #region CRUD operations
        public void Create(TagEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            tagRepository.Create(entity.ToDalTag());
            unitOfWork.Commit();
        }

        public void Update(TagEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            tagRepository.Update(entity.ToDalTag());
            unitOfWork.Commit();
        }

        public void Delete(TagEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            tagRepository.Delete(entity.ToDalTag());
            unitOfWork.Commit();
        }
        #endregion

        #region Get operations
        public IEnumerable<TagEntity> GetAll() => tagRepository.GetAll().Select(t => t.ToBllTag());
        public TagEntity GetById(int id) => tagRepository.GetById(id)?.ToBllTag();
        /// <summary>
        /// This method finds BLL tag by given name.
        /// </summary>
        /// <param name="name">Name of searching tag.</param>
        /// <returns>Returns BLL tag with given name.</returns>
        public TagEntity GetTagEntityByName(string name) => tagRepository.GetTagByName(name)?.ToBllTag();
        #endregion

        private readonly IUnitOfWork unitOfWork;
        private readonly ITagRepository tagRepository;
        private readonly IPostRepository postRepository;
    }
}
