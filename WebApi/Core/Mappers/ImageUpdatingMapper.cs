using Core.Contract.Contract;
using Core.Contract.Models;
using Repository.Models;

namespace Core.Mappers
{
    public class ImageUpdatingMapper : IImageUpdatingMapper
    {
        public Image Update(Image model, ImageView newModel)
        {
            model.Description = newModel.Description;

            return model;
        }
    }
}