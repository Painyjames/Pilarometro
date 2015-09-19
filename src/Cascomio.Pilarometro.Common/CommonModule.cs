using Autofac;
using Cascomio.Pilarometro.Common.Queries;
using Microsoft.Framework.OptionsModel;
using System.Reflection;

namespace Cascomio.Pilarometro.Common
{
    public class CommonModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{

			var assembly = Assembly.GetExecutingAssembly();

			builder.RegisterAssemblyTypes(assembly)
				.AsClosedTypesOf(typeof(NestQuery<>));

			builder.Register(p => p.Resolve<IOptions<ElasticsearchOptions>>().Value).As<ElasticsearchOptions>().SingleInstance();
		}
	}
}
