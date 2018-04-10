using System.Collections.Generic;
using Core.Contract.Models;

namespace Core.Contract.Contract
{
    public interface IImageRetrievingService
    {
        IEnumerable<ImageView> Get();
    }
}