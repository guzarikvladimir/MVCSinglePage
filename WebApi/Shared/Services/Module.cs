using AutoMapper;
using Ninject;

namespace Shared.Services
{
    public abstract class Module
    {
        public abstract void RegisterServices(IKernel kernel);

        public abstract void RegisterMappers(IMapperConfigurationExpression config);
    }
}