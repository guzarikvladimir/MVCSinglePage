using System;
using System.Collections.Generic;
using System.IO;
using AutoMapper;
using Core.Contract.Models;
using Shared.Contract;
using ImageEntity = Repository.Models.Image;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Web.Configuration;
using System.Web.Mvc;
using Core.Contract.Contract;

namespace Core.Mappers
{
    public class ImageMapper : IEntityMapper<ImageEntity, ImageView>
    {
        static readonly Dictionary<string, string> exifProperties = new Dictionary<string, string>()
        {
            { "Model", "0x0110" },
            { "DateAndTime", "0x0132" },
            { "Compression", "0x0103" },
            { "ExposureTime", "0x829A" },
            { "ExifVersion", "0x9000" }
        };

        public static void Register(IMapperConfigurationExpression config)
        {
            config.CreateMap<ImageEntity, ImageView>();
        }

        public ImageView Map(ImageEntity model)
        {
            ImageView imageView = Mapper.Map<ImageView>(model);
            imageView.Url = Path.Combine(@"\ImagesLibrary", imageView.Name);
            imageView.Exif = GetExifData(imageView);

            return imageView;
        }

        private ExifView GetExifData(ImageView imageView)
        {
            string libraryPath = WebConfigurationManager.AppSettings["libraryPath"];
            Image image = new Bitmap(Path.Combine(libraryPath, imageView.Name));
            Dictionary<int, PropertyItem> propertyItems = image.PropertyItems.ToDictionary(p => p.Id);

            return IsEmptyExif(image) ? null : new ExifView()
            {
                Model = GetProperty<string>(exifProperties["Model"], propertyItems),
                DateAndTime = GetProperty<DateTime?>(exifProperties["DateAndTime"], propertyItems),
                Compression = GetProperty<string>(exifProperties["Compression"], propertyItems), // PropertyTagTypeShort idk how it's parsed
                //ExposureTime = GetProperty<double?>(exifProperties["ExposureTime"], propertyItems), // PropertyTagTypeRational
                ExifVersion = GetProperty<double?>(exifProperties["ExifVersion"], propertyItems)
            };
        }

        private T GetProperty<T>(string tag, Dictionary<int, PropertyItem> propertyItems, 
            Func<byte[], string> encoder = null)
        {
            int propid = Convert.ToInt32(tag, 16);
            PropertyItem property;
            if (!propertyItems.TryGetValue(propid, out property))
            {
                return default(T);
            }

            string encodedValue = encoder == null 
                ? Encoding.ASCII.GetString(property.Value) 
                : encoder(property.Value);
            
            return DependencyResolver.Current.GetService<IEncodedValueParserService<T>>().Get(encodedValue);
        }

        private bool IsEmptyExif(Image image)
        {
            return !exifProperties.Values.Any(ep => image.PropertyIdList.Contains(Convert.ToInt32(ep, 16)));
        }
    }
}