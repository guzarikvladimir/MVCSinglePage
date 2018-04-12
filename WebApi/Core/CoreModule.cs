using AutoMapper;
using Core.Contract.Contract;
using Core.Contract.Models;
using Core.Mappers;
using Core.Services;
using Ninject;
using Ninject.Web.Common;
using Repository.Models;
using Shared.Contract;
using Shared.Services;

namespace Core
{
    public class CoreModule : Module
    {
        public override void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IImageRetrievingService>().To<ImageRetrievingService>().InRequestScope();
            kernel.Bind<IEntityMapper<Image, ImageView>, IEntityMapper<ImageView, Image>>()
                .To<ImageMapper>()
                .InRequestScope();
            kernel.Bind<IImageModifyingService>().To<ImageModifyingService>().InRequestScope();
            kernel.Bind<IImageUpdatingMapper>().To<ImageUpdatingMapper>().InRequestScope();
        }

        public override void RegisterMappers(IMapperConfigurationExpression config)
        {
            ImageMapper.Register(config);
        }
    }
}