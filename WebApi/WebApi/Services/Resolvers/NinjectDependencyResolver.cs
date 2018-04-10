using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using AutoMapper;
using Ninject;
using Module = Shared.Services.Module;

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
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            Mapper.Initialize(cfg =>
            {
                foreach (Assembly assembly in assemblies)
                {
                    IEnumerable<Type> moduleTypes = assembly.GetTypes().Where(t => !t.IsAbstract && t.IsSubclassOf(typeof(Module)));
                    foreach (Type moduleType in moduleTypes)
                    {
                        Module module = Activator.CreateInstance(moduleType) as Module;
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