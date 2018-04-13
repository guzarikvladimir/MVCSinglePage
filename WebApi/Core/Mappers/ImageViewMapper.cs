using System;
using AutoMapper;
using Core.Contract.Models;
using Repository.Models;
using Shared.Contract;

namespace Core.Mappers
{
    public class ImageViewMapper : IEntityMapper<ImageView, Image>
    {
        public static void Register(IMapperConfigurationExpression config)
        {
            config.CreateMap<ImageView, Image>();
        }

        public Image Map(ImageView model)
        {
            if (!model.Id.HasValue)
            {
                model.Id = Guid.NewGuid();
                model.CreatedDate = DateTime.Now;
            }

            return Mapper.Map<Image>(model);
        }
    }
}