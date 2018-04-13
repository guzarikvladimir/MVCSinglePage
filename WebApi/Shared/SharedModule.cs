using AutoMapper;
using Ninject;
using Ninject.Web.Common;
using Shared.Contract;
using Shared.Services;

namespace Shared
{
    public class SharedModule : IModule
    {
        public void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IDbFactory>().To<DbFactory>().InRequestScope();
        }

        public void RegisterMappers(IMapperConfigurationExpression config)
        {
        }
    }
}