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
    /// Realization of IPostService interface.
    /// </summary>
    public class PostService : IPostService
    {
        // It will be better to use IRepositoryFactory.
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

            likeRepository.DeleteLikesFromPost(entity.Id);
            commentRepository.DeleteCommentsFromPost(entity.Id);
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
                // Heavy SQL query
                // DatabaseView (Virtual table)
                // Denormalization in the database
                FillPost(bllPost);

                yield return bllPost;
            }
        }

        /// <summary>
        /// This method gets BLL posts for one page by using give parameters such as size of page and number of page.
        /// </summary>
        /// <param name="pageSize">The number of items on one page.</param>
        /// <param name="pageNumber">Number of page.</param>
        /// <returns>Returns collection of BLL posts for one page.</returns>
        public IEnumerable<PostEntity> GetPostsForPage(int pageSize, int pageNumber)
        {
            foreach (var dalPost in postRepository.GetPostsForPage(pageSize, pageNumber))
            {
                var bllPost = dalPost.ToBllPost();
                bllPost.User = userRepository.GetById(dalPost.UserId).ToBllUser();
                FillPost(bllPost);
                yield return bllPost;
            }
        }

        public PostEntity GetById(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            var dalPost = postRepository.GetById(id);

            if (dalPost == null)
                return null;

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
                        Avatar = user.Avatar
                    }
                };

                bllPost.Comments.Add(bllComment);
            }


            foreach (var dalLike in likeRepository.GetDalLikesByPostId(dalPost.Id))
                bllPost.Likes.Add(dalLike.ToBllLike());

            return bllPost;
        }

        /// <summary>
        /// This method finds DAL posts by tag name. 
        /// </summary>
        /// <param name="tagName">Name of tag.</param>
        /// <returns>Returns collection of DAL posts.</returns>
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
                // Heavy SQL query
                // DatabaseView (Virtual table)
                // Denormalization in the database
                FillPost(bllPost);
                bllPosts.Add(bllPost);
            }

            return bllPosts;
        }

        /// <summary>
        /// This method returns collection of DAL likes which belong to the post.
        /// </summary>
        /// <param name="postId">Id of the post.</param>
        /// <returns>Returns collection of DAL posts.</returns>
        public IEnumerable<LikeEntity> GetLikesByPostId(int postId)
        {
            if (postId < 0)
                throw new ArgumentException(nameof(postId));

            return likeRepository.GetDalLikesByPostId(postId).Select(like => like.ToBllLike());
        }

        /// <summary>
        /// This method returns collection of DAL comments which belong to the post.
        /// </summary>
        /// <param name="postId">Id of the post.</param>
        /// <returns>Returns collection of DAL comments.</returns>
        public IEnumerable<CommentEntity> GetCommentsByPostId(int postId)
        {
            return commentRepository.GetDalCommentsByPostId(postId).Select(comment => comment.ToBllComment());
        }
        #endregion

        /// <summary>
        /// This method adds tags to the post.
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="tags"></param>
        public void AddTagsToPost(int postId, string[] tags)
        {
            if (tags == null)
                throw new ArgumentNullException(nameof(tags));

            postRepository.AddTagsToPost(postId, tags);
            unitOfWork.Commit();
        }

        /// <summary>
        /// This method add like to the post or delete like if it already exist.
        /// </summary>
        /// <param name="likeEntity">BLL like.</param>
        /// <returns>Returns true if it was like and false if it was dislike.</returns>
        public bool Like(LikeEntity likeEntity)
        {
            if (likeEntity == null)
                throw new ArgumentNullException(nameof(likeEntity));

            var dalLike = likeRepository.GetDalLikeByPostIdAndUserId(likeEntity.UserId, likeEntity.PostId);

            if (dalLike != null)
            {
                likeRepository.Delete(dalLike);
                unitOfWork.Commit();
                return true;
            }
            else
            {
                likeRepository.Create(likeEntity.ToDalLike());
                unitOfWork.Commit();
                return false;
            }
        }

        // Fill post by comments and likes.
        private void FillPost(PostEntity post)
        {
            foreach (var dalTag in tagRepository.GetTagsByPostId(post.Id))
                post.Tags.Add(dalTag.ToBllTag());

            foreach (var dalComment in commentRepository.GetDalCommentsByPostId(post.Id))
                post.Comments.Add(dalComment.ToBllComment());

            foreach (var dalLike in likeRepository.GetDalLikesByPostId(post.Id))
                post.Likes.Add(dalLike.ToBllLike());
        }

        /// <summary>
        /// This method determines is post was liked.
        /// </summary>
        /// <param name="nickname">Nickname of the user.</param>
        /// <param name="likes">Collection of likes.</param>
        /// <returns>Returns true is post was liked.</returns>
        public bool IsLikedPost(string nickname, IEnumerable<LikeEntity> likes)
        {
            var user = userRepository.GetByNickname(nickname);
            return likes.Any(like => like.UserId == user.Id);
        }

        /// <summary>
        /// This method returns number of posts in the database.
        /// </summary>
        /// <returns>Returns number of posts in the database.</returns>
        public int Count() => postRepository.Count();

        #region Private fields
        private readonly IUnitOfWork unitOfWork;
        private readonly IPostRepository postRepository;
        private readonly IUserRepository userRepository;
        private readonly ICommentRepository commentRepository;
        private readonly ILikeRepository likeRepository;
        private readonly ITagRepository tagRepository;
        #endregion
    }
}