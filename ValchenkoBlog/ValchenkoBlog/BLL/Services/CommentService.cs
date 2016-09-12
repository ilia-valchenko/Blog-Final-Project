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
    public class CommentService : ICommentService
    {
        public CommentService(IUnitOfWork unitOfWork, 
                              ICommentRepository commentRepository,
                              IUserRepository userRepository,
                              IPostRepository postRepository)
        {
            this.unitOfWork = unitOfWork;
            this.commentRepository = commentRepository;
            this.userRepository = userRepository;
            this.postRepository = postRepository;
        }

        #region CRUD operations
        public void Create(CommentEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            commentRepository.Create(entity.ToDalComment());
            unitOfWork.Commit();
        }
        public void Update(CommentEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            commentRepository.Update(entity.ToDalComment());
            unitOfWork.Commit();
        }
        public void Delete(CommentEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            commentRepository.Delete(entity.ToDalComment());
            unitOfWork.Commit();
        }
        #endregion

        #region Get operations
        public IEnumerable<CommentEntity> GetAll() {
            var comments = commentRepository.GetAll().Select(t => t.ToBllComment());

            foreach(var comment in comments)
            {
                comment.User = userRepository.GetById(comment.User.Id).ToBllUser();
                comment.Post = postRepository.GetById(comment.Post.Id).ToBllPost();
                yield return comment;
            }
        } 
        public CommentEntity GetById(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            var dalComment = commentRepository.GetById(id);

            if (dalComment == null)
                return null;

            var bllComment = dalComment.ToBllComment();
            var commentAuthor = userRepository.GetById(dalComment.UserId);
            bllComment.User = new UserEntity
            {
                Id = dalComment.UserId,
                Nickname = commentAuthor.Nickname,
                Avatar = commentAuthor.Avatar
            };
           
            return bllComment;
        }
        public CommentEntity GetOneByPredicate(Expression<Func<CommentEntity, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<CommentEntity, DalUser>(Expression.Parameter(typeof(DalComment), predicates.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<DalComment, bool>>(visitor.Visit(predicates.Body), visitor.NewParameterExp);
            return commentRepository.GetOneByPredicate(exp2).ToBllComment();
        }
        public IEnumerable<CommentEntity> GetAllByPredicate(Expression<Func<CommentEntity, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<CommentEntity, DalComment>(Expression.Parameter(typeof(DalComment), predicates.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<DalComment, bool>>(visitor.Visit(predicates.Body), visitor.NewParameterExp);
            return commentRepository.GetAllByPredicate(exp2).Select(c => c.ToBllComment()).ToList();
        }
        public IEnumerable<CommentEntity> GetCommentsByPostId(int postId)
        {
            var comments = commentRepository.GetDalCommentsByPostId(postId).Select(t => t.ToBllComment());

            foreach (var comment in comments)
            {
                comment.User = userRepository.GetById(comment.User.Id).ToBllUser();
                comment.Post = postRepository.GetById(comment.Post.Id).ToBllPost();
                yield return comment;
            }
        } 
        #endregion

        private readonly IUnitOfWork unitOfWork;
        private readonly ICommentRepository commentRepository;
        private readonly IUserRepository userRepository;
        private readonly IPostRepository postRepository;
    }
}
