using System.Collections.Generic;
using Core.Contract.Contract;
using Core.Contract.Models;
using Ninject;
using Repository;
using Shared.Contract;
using Shared.Services;

namespace Core.Services
{
    public class ImageRetrievingService : IImageRetrievingService
    {
        [Inject]
        public IEntityMapper<Image, ImageView> ImageMappper { get; set; }

        public IEnumerable<ImageView> Get()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (Image image in db.Images)
                {
                    yield return ImageMappper.Map(image);
                }
            }
        }
    }
}