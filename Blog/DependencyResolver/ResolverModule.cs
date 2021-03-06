﻿using System.Data.Entity;
using BLL.Interfacies.Services;
using BLL.Services;
using DAL.Concrete;
using DAL.Interfacies.Repository;
using Ninject;
using Ninject.Web.Common;
using ORM;
using DAL.Interfacies.Repository.ModelRepository;
using DAL.Concrete.ModelRepository;

namespace DependencyResolver
{
    /// <summary>
    /// This is the config class which know how to add Bindings for concrete Types.
    /// </summary>
    public static class ResolverConfig
    {
        public static void ConfigurateResolverWeb(this IKernel kernel)
        {
            Configure(kernel, true);
        }

        public static void ConfigurateResolverConsole(this IKernel kernel)
        {
            Configure(kernel, false);
        }

        private static void Configure(IKernel kernel, bool isWeb)
        {
            if (isWeb)
            {
                kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
                kernel.Bind<DbContext>().To<BlogDbContext>().InRequestScope();
            }
            else
            {
                kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();
                kernel.Bind<DbContext>().To<BlogDbContext>().InSingletonScope();
            }

            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IUserRepository>().To<UserRepository>();

            kernel.Bind<IRoleService>().To<RoleService>();
            kernel.Bind<IRoleRepository>().To<RoleRepository>();

            kernel.Bind<IPostService>().To<PostService>();
            kernel.Bind<IPostRepository>().To<PostRepository>();

            kernel.Bind<ICommentService>().To<CommentService>();
            kernel.Bind<ICommentRepository>().To<CommentRepository>();

            kernel.Bind<ITagService>().To<TagService>();
            kernel.Bind<ITagRepository>().To<TagRepository>();

            kernel.Bind<ILikeRepository>().To<LikeRepository>();
        }
    }
}
