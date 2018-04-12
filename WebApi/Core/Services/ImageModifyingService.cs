using System.Data.Entity;
using Core.Contract.Contract;
using Core.Contract.Models;
using Ninject;
using Repository.Models;
using Shared.Contract;

namespace Core.Services
{
    public class ImageModifyingService : IImageModifyingService
    {
        #region Injects

        [Inject]
        public IEntityMapper<ImageView, Image> ImageMapper { get; set; }

        [Inject]
        public IDbFactory DbFactory { get; set; }

        #endregion

        public void Add(ImageView image)
        {
            Image dbImage = ImageMapper.Map(image);
            using (DbContext db = DbFactory.Create())
            {
                // It is better to make generic base class
                db.Set<Image>().Add(dbImage);

                db.SaveChanges();
            }
        }
    }
}