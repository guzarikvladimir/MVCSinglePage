using System;
using System.IO;
using AutoMapper;
using Core.Contract.Models;
using Shared.Contract;
using ImageEntity = Repository.Models.Image;
using System.Drawing;
using System.Text;
using System.Web.Configuration;

namespace Core.Mappers
{
    public class ImageMapper : IEntityMapper<ImageEntity, ImageView>,
        IEntityMapper<ImageView, ImageEntity>
    {
        public static void Register(IMapperConfigurationExpression config)
        {
            config.CreateMap<ImageEntity, ImageView>();
            config.CreateMap<ImageView, ImageEntity>();
        }

        public ImageView Map(ImageEntity model)
        {
            ImageView imageView = Mapper.Map<ImageView>(model);
            imageView.Url = Path.Combine(@"\ImagesLibrary", imageView.Name);
            //imageView.Exif = GetExifData(imageView);

            return imageView;
        }

        private ExifView GetExifData(ImageView imageView)
        {
            string libraryPath = WebConfigurationManager.AppSettings["libraryPath"];
            Image image = new Bitmap(Path.Combine(libraryPath, imageView.Name));

            return new ExifView()
            {
                Model = Encoding.Default.GetString(image.GetPropertyItem(Convert.ToInt32("0x0110", 16)).Value),
                DataAndTime = DateTime.Parse(Encoding.Default.GetString(image.GetPropertyItem(Convert.ToInt32("0x0132", 16)).Value)),
                Compression = Encoding.Default.GetString(image.GetPropertyItem(Convert.ToInt32("0x0103", 16)).Value),
                ExposureTime = TimeSpan.Parse(Encoding.Default.GetString(image.GetPropertyItem(Convert.ToInt32("0x9000", 16)).Value)),
                ExifVersion = double.Parse(Encoding.Default.GetString(image.GetPropertyItem(Convert.ToInt32("0x0132", 16)).Value))
            };
        }

        public ImageEntity Map(ImageView model)
        {
            if (!model.Id.HasValue)
            {
                model.Id = Guid.NewGuid();
            }

            model.CreatedDate = DateTime.Now;

            return Mapper.Map<ImageEntity>(model);
        }
    }
}