using AutoMapper;
using Core.Contract.Contract;
using Core.Contract.Models;
using Core.Mappers;
using Core.Services;
using Ninject;
using Ninject.Web.Common;
using Repository;
using Shared.Contract;
using Shared.Services;

namespace Core
{
    public class CoreModule : Module
    {
        public override void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IImageRetrievingService>().To<ImageRetrievingService>().InRequestScope();
            kernel.Bind<IEntityMapper<Image, ImageView>>().To<ImageMapper>().InRequestScope();
        }

        public override void RegisterMappers(IMapperConfigurationExpression config)
        {
            ImageMapper.Register(config);
        }
    }
}