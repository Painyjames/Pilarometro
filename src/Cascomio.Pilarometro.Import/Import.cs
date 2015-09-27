using Autofac;
using Cascomio.Pilarometro.Common.Queries;
using Cascomio.Pilarometro.Domain;
using Nest;
using System.Collections.Generic;

namespace Cascomio.Pilarometro.Import
{
	public class Import : IStartable
	{
		private readonly IServiceClient _client;
		private readonly IElasticClient _elasticClient;

		public Import(IServiceClient client, IElasticClient elasticClient)
		{
			_client = client;
			_elasticClient = elasticClient;
		}

		private void Setup()
		{
			if (_elasticClient.IndexExists(Indexes.POINTS_OF_INTEREST).Exists)
				_elasticClient.DeleteIndex(Indexes.POINTS_OF_INTEREST);
			_elasticClient.CreateIndex(Indexes.POINTS_OF_INTEREST);

			var indexDefinition = new RootObjectMapping
			{
				Name = new PropertyNameMarker { Name = Indexes.POINTS_OF_INTEREST },
				Properties = new Dictionary<PropertyNameMarker, IElasticType>()
			};
			_elasticClient.Map<PointOfInterest>(m =>
			{
				return m
				.InitializeUsing(indexDefinition)
				.IdField(id => id.Path("id"))
				.SourceField(s => s
					.Includes(new[] { "*" })
					)
				.Properties(props =>
						props.GeoPoint(s => s
							.Name(p => p.Coordinates)
						)
					);
			});

		}

		public void Start()
		{
			Setup();
			var task = _client.GetPointsOfInterest();
            task.Wait();
			foreach (var pointOfinterest in task.Result)
			{
				_elasticClient.Index(pointOfinterest, i => i.Index(Indexes.POINTS_OF_INTEREST));
            }
		}
	}
}
