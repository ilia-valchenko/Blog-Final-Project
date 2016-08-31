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
    public class PostService : IPostService
    {
        public PostService(IUnitOfWork unitOfWork, IPostRepository postRepository, IUserRepository userRepository, ICommentRepository commentRepository, ILikeRepository likeRepository)
        {
            this.unitOfWork = unitOfWork;
            this.postRepository = postRepository;
            this.userRepository = userRepository;
            this.commentRepository = commentRepository;
            this.likeRepository = likeRepository;
        }

        public void Create(PostEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            postRepository.Create(entity.ToDalPost());
            unitOfWork.Commit();
        }

        public void Update(PostEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            postRepository.Update(entity.ToDalPost());
            unitOfWork.Commit();
        }

        public void Delete(PostEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            postRepository.Delete(entity.ToDalPost());
            unitOfWork.Commit();
        }

        public IEnumerable<PostEntity> GetAll() => postRepository.GetAll().Select(p => p.ToBllPost());

        public PostEntity GetById(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            return postRepository.GetById(id)?.ToBllPost();
        }

        public IEnumerable<PostEntity> GetPostsByTagName(string tagName)
        {
            throw new NotImplementedException();
        }

        public PostEntity GetOneByPredicate(Expression<Func<PostEntity, bool>> predicates)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PostEntity> GetAllByPredicate(Expression<Func<PostEntity, bool>> predicates)
        {
            throw new NotImplementedException();
        }

        public void AddComment(CommentEntity commentEntity)
        {
            throw new NotImplementedException();
        }

        public void RemoveComment(CommentEntity commentEntity)
        {
            throw new NotImplementedException();
        }

        public void AddLike(LikeEntity likeEntity)
        {
            throw new NotImplementedException();
        }

        public void RemoveLike(LikeEntity likeEntity)
        {
            throw new NotImplementedException();
        }

        private readonly IUnitOfWork unitOfWork;
        private readonly IPostRepository postRepository;
        private readonly IUserRepository userRepository;
        private readonly ICommentRepository commentRepository;
        private readonly ILikeRepository likeRepository;
    }
}
