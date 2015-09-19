using Autofac;
using System;

namespace Cascomio.Pilarometro.Common
{
	public static class ContainerBuilderExtensions
	{
		public static ContainerBuilder AddDataCommon(this ContainerBuilder self)
		{
			self.RegisterModule<CommonModule>();
			return self;
		}
	}
}
