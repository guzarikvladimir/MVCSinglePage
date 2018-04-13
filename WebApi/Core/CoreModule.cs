using System;
using AutoMapper;
using Core.Contract.Contract;
using Core.Contract.Models;
using Core.Mappers;
using Core.Services;
using Core.Services.PropertySelectors;
using Ninject;
using Ninject.Web.Common;
using Repository.Models;
using Shared.Contract;
using Shared.Services;

namespace Core
{
    public class CoreModule : IModule
    {
        public void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IImageRetrievingService>().To<ImageRetrievingService>().InRequestScope();
            kernel.Bind<IImageModifyingService>().To<ImageModifyingService>().InRequestScope();
            
            kernel.Bind<IEntityMapper<Image, ImageView>>().To<ImageMapper>().InRequestScope();
            kernel.Bind<IEntityMapper<ImageView, Image>>().To<ImageViewMapper>().InRequestScope();
            kernel.Bind<IImageUpdatingMapper>().To<ImageUpdatingMapper>().InRequestScope();

            RegisterPropertySelectors(kernel);
        }

        public void RegisterMappers(IMapperConfigurationExpression config)
        {
            ImageMapper.Register(config);
        }

        private void RegisterPropertySelectors(IKernel kernel)
        {
            kernel.Bind<IEncodedValueParserService<DateTime?>>()
                .To<DateTimeEncodedValueParserService>()
                .InRequestScope();
            kernel.Bind<IEncodedValueParserService<double?>>()
                .To<DoubleEncodedValueParserService>()
                .InRequestScope();
            kernel.Bind<IEncodedValueParserService<string>>()
                .To<StringEncodedValueParserService>()
                .InRequestScope();
        }
    }
}