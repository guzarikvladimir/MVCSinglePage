using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApi
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            SetLibraryPath();
        }

        private void SetLibraryPath()
        {
            WebConfigurationManager.AppSettings["libraryPath"] = Server.MapPath("ImagesLibrary");
        }
    }
}
