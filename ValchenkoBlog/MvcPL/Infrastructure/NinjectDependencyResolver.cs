using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DependencyResolver;
using Ninject;

namespace MvcPL.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        public NinjectDependencyResolver(IKernel kernel)
        {
            this.kernel = kernel;
            kernel.ConfigurateResolverWeb();
        }

        public object GetService(Type serviceType) => kernel.TryGet(serviceType);
        public IEnumerable<object> GetServices(Type serviceType) => kernel.GetAll(serviceType);

        private IKernel kernel;
    }
}