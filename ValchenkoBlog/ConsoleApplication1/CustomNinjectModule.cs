using Ninject.Modules;
using BLL.Interfacies.Services;
using BLL.Services;
using DAL.Interfacies.Repository;
using DAL.Concrete;
using DAL.Interfacies.Repository.ModelRepository;
using DAL.Concrete.ModelRepository;
using System.Data.Entity;
using Ninject.Web.Common;
using ORM;

namespace ConsoleApplication1
{
    public class CustomNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IUserService>().To<UserService>();
            this.Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();

            this.Bind<ICommentRepository>().To<CommentRepository>();
            this.Bind<ILikeRepository>().To<LikeRepository>();
            this.Bind<IPostRepository>().To<PostRepository>();
            this.Bind<IRoleRepository>().To<RoleRepository>();
            this.Bind<ITagRepository>().To<TagRepository>();
            this.Bind<IUserRepository>().To<UserRepository>();

            this.Bind<DbContext>().To<BlogDbContext>().InSingletonScope();
        }
    }
}
