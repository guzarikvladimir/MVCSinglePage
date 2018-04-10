using System.Web.Mvc;
using Core.Contract.Contract;
using Ninject;

namespace WebApi.Controllers
{
    public class HomeController : Controller
    {
        [Inject]
        public IImageRetrievingService ImageRetrievingService { get; set; }

        public ActionResult Index()
        {
            return View(ImageRetrievingService.Get());
        }
    }
}