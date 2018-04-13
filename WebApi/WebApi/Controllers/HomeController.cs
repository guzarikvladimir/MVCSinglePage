using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Core.Contract.Contract;
using Core.Contract.Models;
using Ninject;

namespace WebApi.Controllers
{
    public class HomeController : Controller
    {
        #region Injects

        [Inject]
        public IImageRetrievingService ImageRetrievingService { get; set; }

        [Inject]
        public IImageModifyingService ImageModifyingService { get; set; }

        #endregion

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // Business logic requires getting all images after all uploads,
        // So we return images at once not to request server again
        [HttpPost]
        public JsonResult Upload()
        {
            HttpPostedFileBase file = Request.Files[0];
            string fileName = file.FileName;
            string libraryPath = WebConfigurationManager.AppSettings["libraryPath"];
            file.SaveAs(Path.Combine(libraryPath, fileName));
            ImageModifyingService.Add(new ImageView(fileName));

            return Json(GetImages(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Get()
        {
            return Json(GetImages(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public EmptyResult UpdateImage(ImageView image)
        {
            ImageModifyingService.Update(image);

            return new EmptyResult();
        }

        private List<ImageView> GetImages()
        {
            List<ImageView> images = ImageRetrievingService.Get()
                .OrderByDescending(i => i.CreatedDate)
                .ToList();

            return images;
        }
    }
}