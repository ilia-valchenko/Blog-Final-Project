using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using DAL.Interfacies.Repository;
using DAL.Interfacies.Repository.ModelRepository;
using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;
using BLL.Mappers;
using System.IO;

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
        public void Create(PostEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            // CREATE and UPDATE should return ID or created entity.
            postRepository.Create(entity.ToDalPost(), entity.Tags.Select(tag => tag.ToDalTag()));
            unitOfWork.Commit();  
        }

        public void Update(PostEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            postRepository.Update(entity.ToDalPost(), entity.Tags.Select(tag => tag.ToDalTag()));
            unitOfWork.Commit();
        }

        public void Delete(PostEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            // test
            /*var likes = likeRepository.GetDalLikesByPostId(entity.Id);
            if (likes != null)
                foreach (var dalLike in likes)
                    likeRepository.Delete(dalLike);*/
            // end of test

            postRepository.Delete(entity.ToDalPost());
            unitOfWork.Commit();
        }
        #endregion

        #region Get operations
        public IEnumerable<PostEntity> GetAll()
        {
            foreach (var dalPost in postRepository.GetAll())
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

        public PostEntity GetById(int? id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            // Like guys from micrisoft.
            if (id == null)
                return null;

            var dalPost = postRepository.GetById(id);
            var bllPost = dalPost.ToBllPost();
            bllPost.User = userRepository.GetById(dalPost.UserId).ToBllUser();

            foreach (var dalTag in tagRepository.GetTagsByPostId(bllPost.Id))
                bllPost.Tags.Add(dalTag.ToBllTag());

            foreach (var dalComment in commentRepository.GetDalCommentsByPostId(dalPost.Id))
            {
                var user = userRepository.GetById(dalComment.UserId);

                var bllComment = new CommentEntity
                {
                    Id = dalComment.Id,
                    Text = dalComment.Text,
                    PublishDate = dalComment.PublishDate,
                    User = new UserEntity
                    {
                        Id = user.Id,
                        Nickname = user.Nickname,
                        Email = user.Email,
                        // Avatar
                    }
                };

                /*if(user.Avatar != null)
                {
                    using (var ms = new MemoryStream(user.Avatar))
                    {
                        return Image.FromStream(ms);
                    }
                }*/

                bllPost.Comments.Add(bllComment);
            }
                

            foreach (var dalLike in likeRepository.GetDalLikesByPostId(dalPost.Id))
                bllPost.Likes.Add(dalLike.ToBllLike());             

            return bllPost;
        }
        
        public IEnumerable<PostEntity> GetPostsByTagName(string tagName)
        {
            var posts = postRepository.GetDalPostsByTagName(tagName);

            if (posts == null)
                return null;

            var bllPosts = new List<PostEntity>();

            foreach (var dalPost in posts)
            {
                var bllPost = dalPost.ToBllPost();

                bllPost.User = userRepository.GetById(dalPost.UserId).ToBllUser();

                foreach (var dalTag in tagRepository.GetTagsByPostId(bllPost.Id))
                    bllPost.Tags.Add(dalTag.ToBllTag());

                foreach (var dalComment in commentRepository.GetDalCommentsByPostId(dalPost.Id))
                    bllPost.Comments.Add(dalComment.ToBllComment());

                foreach (var dalLike in likeRepository.GetDalLikesByPostId(dalPost.Id))
                    bllPost.Likes.Add(dalLike.ToBllLike());

                bllPosts.Add(bllPost);
            }

            return bllPosts;
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
        public void Like(LikeEntity likeEntity)
        {
            // if null
            var dalLike = likeRepository.GetDalLikeByPostIdAndUserId(likeEntity.UserId, likeEntity.PostId);
            if (dalLike != null)
                likeRepository.Delete(dalLike);
            else
                likeRepository.Create(likeEntity.ToDalLike());

            unitOfWork.Commit();
        }

        public void AddComment(CommentEntity commentEntity)
        {
            // if null
            commentRepository.Create(commentEntity.ToDalComment());
            unitOfWork.Commit();
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