using Cascomio.Pilarometro.Common.Config;
using Cascomio.Pilarometro.Common.Queries;
using Cascomio.Pilarometro.Domain;
using Nest;
using System.Collections.Generic;

namespace Cascomio.Pilarometro.Api.Config
{
    public class ElasticSearchConfigurer : IConfigurer
    {
		private readonly IElasticClient _elasticClient;

		public ElasticSearchConfigurer(IElasticClient elasticClient)
		{
			_elasticClient = elasticClient;
		}

		public void Configure()
		{
			if (!_elasticClient.IndexExists(Indexes.PILAROMETRO).Exists)
			{
				_elasticClient.CreateIndex(Indexes.PILAROMETRO);
				var indexDefinition = new RootObjectMapping
				{
					Name = new PropertyNameMarker { Name = Indexes.POINTS_OF_INTEREST },
					Properties = new Dictionary<PropertyNameMarker, IElasticType>()
				};
				_elasticClient.Map<User>(m =>
				{
					return m
					.InitializeUsing(indexDefinition)
					.IdField(id => id.Path("id"))
					.SourceField(s => s
						.Includes(new[] { "*" })
						);
				});
			}
		}
    }
}
