using Autofac;
using AutoMapper;
using Cascomio.Pilarometro.Common.Commands;
using Cascomio.Pilarometro.Common.Config;
using Cascomio.Pilarometro.Common.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Cascomio.Pilarometro.Api.Modules
{
    public class ApiModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{

			var assembly = Assembly.GetAssembly(typeof(ApiModule));

			builder.RegisterAssemblyTypes(assembly).AssignableTo<IConfigurer>().As<IConfigurer>();

			builder.RegisterAssemblyTypes(assembly)
				.AsClosedTypesOf(typeof(Command<,>));
			builder.RegisterAssemblyTypes(assembly)
				.AsClosedTypesOf(typeof(NestQuery<>));

			builder.RegisterAssemblyTypes(assembly).As<Profile>();
			builder.Register(t => Mapper.Engine).As<IMappingEngine>().OnActivated(x =>
			{

				foreach (var profile in x.Context.Resolve<IEnumerable<Profile>>())
				{
					Mapper.AddProfile(profile);
				}
			}).SingleInstance();
		}
	}
}
