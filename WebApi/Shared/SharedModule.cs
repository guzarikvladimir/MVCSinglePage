using AutoMapper;
using Ninject;
using Ninject.Web.Common;
using Shared.Contract;
using Shared.Services;

namespace Shared
{
    public class SharedModule : Module
    {
        public override void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IDbFactory>().To<DbFactory>().InRequestScope();
        }

        public override void RegisterMappers(IMapperConfigurationExpression config)
        {
        }
    }
}