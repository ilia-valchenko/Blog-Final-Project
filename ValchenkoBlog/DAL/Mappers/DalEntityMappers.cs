//using System.Linq;
//using DAL.Interfacies.DTO;
//using ORM.Models;

//namespace DAL.Mappers
//{
//    public static class DalEntityMappers
//    {
//        #region Comment
//        public static DalComment ToDalComment(this Comment comment)
//        {
//            return new DalComment
//            {
//                Id = comment.CommentId,
//                Text = comment.Text,
//                PublishDate = comment.PublishDate,
//                //Post = comment.Post.ToDalPost(),
//                PostId = comment.Post.PostId,
//                //User = comment.User.ToDalUser()
//                UserId = comment.User.UserId
//            };
//        }

//        public static Comment ToOrmComment(this DalComment dalComment)
//        {
//            return new Comment
//            {
//                CommentId = dalComment.Id,
//                Text = dalComment.Text,
//                PublishDate = dalComment.PublishDate
//                // Isn't necessary?
//                //Post
//                //User
//            };
//        }
//        #endregion

//        #region Like
//        public static DalLike ToDalLike(this Like like)
//        {
//            return new DalLike
//            {
//                Id = like.LikeId,
//                //Post = like.Post.ToDalPost(),
//                PostId = like.Post.PostId,
//                //User = like.User.ToDalUser()
//                UserId = like.User.UserId
//            };
//        }

//        public static Like ToOrmLike(this DalLike dalLike)
//        {
//            return new Like
//            {
//                LikeId = dalLike.Id
//                //Post = dalLike.Post.ToOrmPost(),
//                //User = dalLike.User.ToOrmUser()
//            };
//        }
//        #endregion

//        #region Post
//        public static DalPost ToDalPost(this Post post)
//        {
//            return new DalPost
//            {
//                Id = post.PostId,
//                Title = post.Title,
//                Description = post.Description,
//                PublishDate = post.PublishDate,
//                //User = post.User.ToDalUser()
//                UserId = post.User.UserId
//            };

//            //foreach (var tag in post.Tags)
//            //    newDalPost.Tags.Add(tag.ToDalTag());

//            //foreach (var comment in post.Comments)
//            //    newDalPost.Comments.Add(comment.ToDalComment());

//            //foreach (var like in post.Likes)
//            //    newDalPost.Likes.Add(like.ToDalLike());

//            //return newDalPost;
//        }

//        public static Post ToOrmPost(this DalPost dalPost)
//        {
//            return new Post
//            {
//                PostId = dalPost.Id,
//                Title = dalPost.Title,
//                Description = dalPost.Description,
//                PublishDate = dalPost.PublishDate,
//                //User = dalPost.User.ToOrmUser()
//            };

//            //foreach (var tag in dalPost.Tags)
//            //    newPost.Tags.Add(tag.ToOrmTag());

//            //foreach (var comment in dalPost.Comments)
//            //    newPost.Comments.Add(comment.ToOrmComment());

//            //foreach (var like in dalPost.Likes)
//            //    newPost.Likes.Add(like.ToOrmLike());

//            //return newPost;
//        }
//        #endregion

//        #region Role
//        public static DalRole ToDalRole(this Role role)
//        {
//            return new DalRole
//            {
//                Id = role.RoleId,
//                Name = role.Name,
//                UserId = role.Users
//            };

//            //foreach (var user in role.Users)
//            //    newDalRole.Users.Add(user.ToDalUser());

//            //return newDalRole;
//        }

//        public static Role ToOrmRole(this DalRole dalRole)
//        {
//            var newRole = new Role
//            {
//                RoleId = dalRole.Id,
//                Name = dalRole.Name
//            };

//            foreach (var user in dalRole.Users)
//                newRole.Users.Add(user.ToOrmUser());

//            return newRole;
//        }
//        #endregion

//        #region Tag
//        public static DalTag ToDalTag(this Tag tag)
//        {
//            var newDalTag = new DalTag
//            {
//                Id = tag.TagId,
//                Name = tag.Name
//            };

//            foreach (var post in tag.Posts)
//                newDalTag.Posts.Add(post.ToDalPost());

//            return newDalTag;
//        }

//        public static Tag ToOrmTag(this DalTag dalTag)
//        {
//            var newTag = new Tag
//            {
//                TagId = dalTag.Id,
//                Name = dalTag.Name
//            };

//            foreach (var post in dalTag.Posts)
//                newTag.Posts.Add(post.ToOrmPost());

//            return newTag;
//        }
//        #endregion

//        #region User
//        public static DalUser ToDalUser(this User user)
//        {
//            var newDalUser = new DalUser
//            {
//                Id = user.UserId,
//                Nickname = user.Nickname,
//                Password = user.Password,
//                Avatar = user.Avatar
//            };

//            foreach (var role in user.Roles)
//                newDalUser.Roles.Add(role.ToDalRole());

//            foreach (var post in user.Posts)
//                newDalUser.Posts.Add(post.ToDalPost());

//            foreach (var comment in user.Comments)
//                newDalUser.Comments.Add(comment.ToDalComment());

//            foreach (var like in user.Likes)
//                newDalUser.Likes.Add(like.ToDalLike());

//            return newDalUser;
//        }

//        public static User ToOrmUser(this DalUser dalUser)
//        {
//            var newUser = new User
//            {
//                UserId = dalUser.Id,
//                Nickname = dalUser.Nickname,
//                Password = dalUser.Password,
//                Avatar = dalUser.Avatar
//            };

//            foreach (var role in dalUser.Roles)
//                newUser.Roles.Add(role.ToOrmRole());

//            foreach (var post in dalUser.Posts)
//                newUser.Posts.Add(post.ToOrmPost());

//            foreach (var comment in dalUser.Comments)
//                newUser.Comments.Add(comment.ToOrmComment());

//            foreach (var like in dalUser.Likes)
//                newUser.Likes.Add(like.ToOrmLike());

//            return newUser;
//        } 
//        #endregion
//    }
//}
