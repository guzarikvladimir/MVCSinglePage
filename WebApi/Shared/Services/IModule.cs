using AutoMapper;
using Ninject;

namespace Shared.Services
{
    public interface IModule
    {
        void RegisterServices(IKernel kernel);

        void RegisterMappers(IMapperConfigurationExpression config);
    }
}