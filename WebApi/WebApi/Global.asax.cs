using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Repository;

namespace WebApi
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //DbInit();
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
                        Name = "Image1"
                    },
                    new Image()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Image2"
                    }
                };

                db.Images.AddRange(newModels);
                db.SaveChanges();
            }
        }
    }
}
