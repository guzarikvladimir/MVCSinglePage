using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using AutoMapper;
using Ninject;
using Shared.Services;

namespace WebApi.Services.Resolvers
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            this.kernel = kernel;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            // For best performnce recommended to name custom services with specific symbols, for example CS.
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            Mapper.Initialize(cfg =>
            {
                foreach (Assembly assembly in assemblies)
                {
                    IEnumerable<Type> moduleTypes = assembly.GetTypes()
                        .Where(t => !t.IsAbstract && t.GetInterface("IModule") != null);
                    foreach (Type moduleType in moduleTypes)
                    {
                        IModule module = Activator.CreateInstance(moduleType) as IModule;
                        if (module != null)
                        {
                            module.RegisterServices(kernel);
                            module.RegisterMappers(cfg);
                        }
                    }
                }
            });
        }
    }
}