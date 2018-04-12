using System.Collections.Generic;
using System.Data.Entity;
using Core.Contract.Contract;
using Core.Contract.Models;
using Ninject;
using Repository.Models;
using Shared.Contract;

namespace Core.Services
{
    public class ImageRetrievingService : IImageRetrievingService
    {
        #region Injects

        [Inject]
        public IEntityMapper<Image, ImageView> ImageMapper { get; set; }

        [Inject]
        public IDbFactory DbFactory { get; set; }

        #endregion

        public IEnumerable<ImageView> Get()
        {
            using (DbContext db = DbFactory.Create())
            {
                foreach (Image image in db.Set<Image>())
                {
                    yield return ImageMapper.Map(image);
                }
            }
        }
    }
}