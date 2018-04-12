using System;
using System.Collections.Generic;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using Repository.Models;
using Repository.Services;

namespace WebApi
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //DbInit();
            SetLibraryPath();
        }

        private void SetLibraryPath()
        {
            WebConfigurationManager.AppSettings["libraryPath"] = Server.MapPath("ImagesLibrary");
        }

        private void DbInit()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var oldImages = db.Images;
                db.Images.RemoveRange(oldImages);

                var newModels = new List<Image>
                {
                    new Image()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Image1.jpg"
                    },
                    new Image()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Image2.jpg"
                    }
                };

                db.Images.AddRange(newModels);
                db.SaveChanges();
            }
        }
    }
}
