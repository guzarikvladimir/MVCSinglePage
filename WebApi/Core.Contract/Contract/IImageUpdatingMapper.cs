using Core.Contract.Models;
using Repository.Models;

namespace Core.Contract.Contract
{
    public interface IImageUpdatingMapper
    {
        Image Update(Image model, ImageView newModel);
    }
}