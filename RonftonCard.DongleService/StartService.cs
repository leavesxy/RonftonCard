using Owin;
using System.Web.Http;
using System.Web.Http.Cors;

namespace RonftonCard.DongleService
{
	public class StartService
	{
		public void Configuration(IAppBuilder appBuilder)
		{
			// Configure Web API for self-host. 
			HttpConfiguration config = new HttpConfiguration();

			var cors = new EnableCorsAttribute("*", "*", "*");

			config.EnableCors(cors);

			// enable specified routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			appBuilder.UseWebApi(config);
		}
	}
}
