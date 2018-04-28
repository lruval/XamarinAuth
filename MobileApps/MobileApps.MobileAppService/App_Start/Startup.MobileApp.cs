using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using Owin;

using MobileApps.MobileAppService.DataObjects;
using MobileApps.MobileAppService.Models;

namespace MobileApps.MobileAppService
{
    public partial class Startup
    {
        public static void ConfigureMobileApp(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            //For more information on Web API tracing, see http://go.microsoft.com/fwlink/?LinkId=620686 
            config.EnableSystemDiagnosticsTracing();

            new MobileAppConfiguration()
                .UseDefaultConfiguration()
                .ApplyTo(config);

            // Use Entity Framework Code First to create database tables based on your DbContext
            Database.SetInitializer(new DatabaseInitializer());

            // To prevent Entity Framework from modifying your database schema, use a null database initializer
            // Database.SetInitializer<templateitemsContext>(null);

            MobileAppSettingsDictionary settings = config.GetMobileAppSettingsProvider().GetMobileAppSettings();

            if (string.IsNullOrEmpty(settings.HostName))
            {
                // This middleware is intended to be used locally for debugging. By default, HostName will
                // only have a value when running in an App Service application.
                app.UseAppServiceAuthentication(new AppServiceAuthenticationOptions
                {
                    SigningKey = ConfigurationManager.AppSettings["SigningKey"],
                    ValidAudiences = new[] { ConfigurationManager.AppSettings["ValidAudience"] },
                    ValidIssuers = new[] { ConfigurationManager.AppSettings["ValidIssuer"] },
                    TokenHandler = config.GetAppServiceTokenHandler()
                });
            }
            app.UseWebApi(config);

        }
    }

    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<MasterDetailContext>
    {
        protected override void Seed(MasterDetailContext context)
        {
            List<Boletas> boletas = new List<Boletas>
            {
                new Boletas {Id = Guid.NewGuid().ToString(), coordx = "0.1", coordy = "0.2", descripcion = "hackfest1", firmaUrl="http://hackfest.io/photo1.jpg", foto1Url= "http://hackfest.io/photo1.jpg" , foto2Url="http://hackfest.io/photo1.jpg", idBoleta = "1", tecnicoId = "1" },
                new Boletas {Id = Guid.NewGuid().ToString(), coordx = "0.2", coordy = "0.2", descripcion = "hackfest2", firmaUrl="http://hackfest.io/photo1.jpg", foto1Url= "http://hackfest.io/photo1.jpg" , foto2Url="http://hackfest.io/photo1.jpg", idBoleta = "2", tecnicoId = "2" },
                new Boletas {Id = Guid.NewGuid().ToString(), coordx = "0.3", coordy = "0.3", descripcion = "hackfest3", firmaUrl="http://hackfest.io/photo1.jpg", foto1Url= "http://hackfest.io/photo1.jpg" , foto2Url="http://hackfest.io/photo1.jpg", idBoleta = "3", tecnicoId = "3" },

            };

            foreach (Boletas boleta in boletas)
            {
                context.Set<Boletas>().Add(boleta);
            }

            base.Seed(context);
        }
    }
}