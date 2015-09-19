using Microsoft.AspNet.Hosting;
using Microsoft.Dnx.Runtime;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Framework.Configuration;
using Autofac.Framework.DependencyInjection;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.Logging;

namespace Cascomio.Pilarometro.Api
{
	public class Startup
	{

		private IContainer _container;
		public IConfigurationRoot Configuration { get; set; }

		public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
		{
		}

		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();
			var builder = new ContainerBuilder();
			builder.Populate(services);
			
			builder.RegisterInstance(Configuration).As<IConfiguration>();

			_container = builder.Build();

			return _container.Resolve<IServiceProvider>();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerfactory, ILogger<Startup> logger)
		{
			logger.LogInformation("started");
			
			app.UseStaticFiles()
				.UseMvc(routes =>
				{

					routes.MapRoute(
						name: "default",
						template: "api/{controller}/{action}/{id?}");
				});
		}

	}
}
