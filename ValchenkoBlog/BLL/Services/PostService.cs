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
using System.Diagnostics;

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

        public void Create(PostEntity entity, string[] tags)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var post = entity.ToDalPost();
            postRepository.Create(post);
            // just add tags
            Debug.Write("\nADD TAG TO POST. PostId: " + post.Id);
            postRepository.AddTagsToPost(post.Id, tags);
            unitOfWork.Commit();
        }

        // WHAT SHOULD I DO WITH IT?
        public void Create(PostEntity entity)
        {
            throw new NotImplementedException();
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

        public void AddTagsToPost(int postId, string[] tags)
        {
            if (postId < 0)
                throw new ArgumentException(nameof(postId));

            if (tags == null)
                throw new ArgumentNullException(nameof(tags));

            postRepository.AddTagsToPost(postId, tags);
            unitOfWork.Commit();
        }

        public IEnumerable<LikeEntity> GetLikesByPostId(int postId)
        {
            if (postId < 0)
                throw new ArgumentException(nameof(postId));

            return likeRepository.GetDalLikesByPostId(postId).Select(like => like.ToBllLike());
        }

        public IEnumerable<CommentEntity> GetCommentsByPostId(int postId)
        {
            if (postId < 0)
                throw new ArgumentException(nameof(postId));

            return commentRepository.GetDalCommentsByPostId(postId).Select(comment => comment.ToBllComment());
        }

        private readonly IUnitOfWork unitOfWork;
        private readonly IPostRepository postRepository;
        private readonly IUserRepository userRepository;
        private readonly ICommentRepository commentRepository;
        private readonly ILikeRepository likeRepository;
    }
}
