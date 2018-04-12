using System.Collections.Generic;
using System.IO;
using Core.Contract.Contract;
using Core.Contract.Models;
using Ninject;

namespace Core.Services
{
    public class ImageService
    {
        [Inject]
        public IImageRetrievingService ImageRetrievingService { get; set; }

        public void Get()
        {
            IEnumerable<ImageView> images = ImageRetrievingService.Get();
        }
    }
}