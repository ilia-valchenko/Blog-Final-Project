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
        public PostService(IUnitOfWork unitOfWork, 
                           IPostRepository postRepository, 
                           IUserRepository userRepository, 
                           ICommentRepository commentRepository, 
                           ILikeRepository likeRepository,
                           ITagRepository tagRepository)
        {
            this.unitOfWork = unitOfWork;
            this.postRepository = postRepository;
            this.userRepository = userRepository;
            this.commentRepository = commentRepository;
            this.likeRepository = likeRepository;
            this.tagRepository = tagRepository;
        }

        #region CRUD operations
        public int Create(PostEntity entity/*, string[] tagsName*/)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            
            //var post = entity.ToDalPost();

            /*var dalTags = new List<DalTag>();
            foreach (var name in tagsName)
            {
                var tag = tagRepository.GetTagByName(name);

                if (tag != null)
                    dalTags.Add(tag);
            }*/

            // CREATE and UPDATE should return ID of created entity.
            //postRepository.Create(post/*, dalTags*/);
            int createdPostId = postRepository.Create(entity.ToDalPost());


            // I don't know the future Id of the post.
            //postRepository.AddTagsToPost(post.Id, tags);

            unitOfWork.Commit();
            // Add new
            return createdPostId;
        }

        public int Update(PostEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            int updatedPostId = postRepository.Update(entity.ToDalPost());
            unitOfWork.Commit();
            return updatedPostId;
        }

        public void Delete(PostEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            postRepository.Delete(entity.ToDalPost());
            unitOfWork.Commit();
        }
        #endregion

        #region Get operations
        public IEnumerable<PostEntity> GetAll()
        {
            foreach(var dalPost in postRepository.GetAll())
            {
                var bllPost = dalPost.ToBllPost();

                bllPost.User = userRepository.GetById(dalPost.UserId).ToBllUser();

                foreach (var dalTag in tagRepository.GetTagsByPostId(bllPost.Id))
                    bllPost.Tags.Add(dalTag.ToBllTag());

                foreach (var dalComment in commentRepository.GetDalCommentsByPostId(dalPost.Id))
                    bllPost.Comments.Add(dalComment.ToBllComment());

                foreach (var dalLike in likeRepository.GetDalLikesByPostId(dalPost.Id))
                    bllPost.Likes.Add(dalLike.ToBllLike());

                yield return bllPost;
            }
        } 

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
        #endregion

        public void AddTagsToPost(int postId, string[] tags)
        {
            if (postId < 0)
                throw new ArgumentOutOfRangeException(nameof(postId));

            if (tags == null)
                throw new ArgumentNullException(nameof(tags));

            postRepository.AddTagsToPost(postId, tags);
            unitOfWork.Commit();
        }
        public void AddLike(LikeEntity likeEntity)
        {
            likeRepository.Create(likeEntity.ToDalLike());
        }

        public void RemoveLike(LikeEntity likeEntity)
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

        private readonly IUnitOfWork unitOfWork;
        private readonly IPostRepository postRepository;
        private readonly IUserRepository userRepository;
        private readonly ICommentRepository commentRepository;
        private readonly ILikeRepository likeRepository;
        private readonly ITagRepository tagRepository;
    }
}
