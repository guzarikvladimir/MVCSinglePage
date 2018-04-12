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

        [HttpPost]
        public EmptyResult Upload()
        {
            HttpPostedFileBase file = Request.Files[0];
            string fileName = file.FileName;
            string libraryPath = WebConfigurationManager.AppSettings["libraryPath"];
            file.SaveAs(Path.Combine(libraryPath, fileName));
            ImageModifyingService.Add(new ImageView(fileName));

            return new EmptyResult();
        }

        [HttpGet]
        public JsonResult Get()
        {
            List<ImageView> images = ImageRetrievingService.Get()
                .OrderByDescending(i => i.CreatedDate)
                .ToList();

            return Json(images, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public EmptyResult UpdateImage(ImageView image)
        {
            ImageModifyingService.Update(image);

            return new EmptyResult();
        }
    }
}