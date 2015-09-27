using Microsoft.AspNet.Hosting;
using Microsoft.Dnx.Runtime;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Microsoft.Framework.Configuration;
using Autofac.Framework.DependencyInjection;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.Logging;
using Cascomio.Pilarometro.Common;
using Cascomio.Pilarometro.Common.Config;
using System.Reflection;
using Cascomio.Pilarometro.Api.Modules;

namespace Cascomio.Pilarometro.Api
{
	public class Startup
	{

		private IContainer _container;
		public IConfigurationRoot Configuration { get; set; }

		public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
		{
			var basePath = appEnv.ApplicationBasePath;
			Configuration = new ConfigurationBuilder()
				.AddJsonFile(basePath + "/App_Data/Development.json")
				.Build();
        }

		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();
			services
			.Configure<ElasticsearchOptions>(options =>
			{
				options.Url = Configuration.GetSection("elasticsearch:endpoints").Value.Split(',').Select(s => new Uri(s)).ToList();
			});

			var assembly = Assembly.GetExecutingAssembly();
            var builder = new ContainerBuilder();
			builder.Populate(services);
			builder.RegisterModule<CommonModule>();
			builder.RegisterModule<ApiModule>();
			builder.RegisterInstance(Configuration).As<IConfiguration>();
			_container = builder.Build();

			return _container.Resolve<IServiceProvider>();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerfactory, ILogger<Startup> logger)
		{
			var configurers = app.ApplicationServices.GetService<IEnumerable<IConfigurer>>();
            foreach (var configurer in configurers)
			{
				configurer.Configure();
            }

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
