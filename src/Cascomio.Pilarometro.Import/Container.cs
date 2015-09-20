using Autofac;
using Cascomio.Pilarometro.Common;
using Microsoft.Framework.OptionsModel;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cascomio.Pilarometro.Import
{
	public class Container
	{
		public static void Start()
		{
			var builder = new ContainerBuilder();

			builder.RegisterType<ServiceClient>().As<IServiceClient>().SingleInstance();
			builder.RegisterType<Import>()
			.As<IStartable>()
			.AutoActivate()
			.SingleInstance();
			builder.Register(p => new ElasticClient(
				//TODO: get this from config file
				new ConnectionSettings(new Uri("http://localhost:9200"))
			))
			.As<IElasticClient>().SingleInstance();

			var container = builder.Build();

			container.BeginLifetimeScope();
		}
	}
}
