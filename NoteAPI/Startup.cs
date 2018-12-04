using System.Web.Http;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Cors;
using System.Configuration;

[assembly: OwinStartup(typeof(NoteAPI.Startup))]

namespace NoteAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
           
            var config = new HttpConfiguration();          

            config.MapHttpAttributeRoutes();

            // require authentication for all controllers
            config.Filters.Add(new AuthorizeAttribute());            

            app.UseWebApi(config);

            app.UseCors(CorsOptions.AllowAll);

            config.EnsureInitialized();

            log4net.Config.BasicConfigurator.Configure();
        }
    }
}