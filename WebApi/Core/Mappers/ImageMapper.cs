using AutoMapper;
using Core.Contract.Models;
using Repository;
using Shared.Contract;

namespace Core.Mappers
{
    public class ImageMapper : IEntityMapper<Image, ImageView>
    {
        public static void Register(IMapperConfigurationExpression config)
        {
            config.CreateMap<Image, ImageView>();
        }

        public ImageView Map(Image model)
        {
            return Mapper.Map<ImageView>(model);
        }
    }
}