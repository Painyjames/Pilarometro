using Autofac;
using Elasticsearch.Net;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cascomio.Pilarometro.Import
{
	public class Import : IStartable
	{
		private readonly IServiceClient _client;
		private readonly IElasticClient _elasticClient;

		private const string Index = "points_of_interest";

		public Import(IServiceClient client, IElasticClient elasticClient)
		{
			_client = client;
			_elasticClient = elasticClient;
		}

		private void Setup()
		{

			if (_elasticClient.IndexExists(Index).Exists)
				_elasticClient.DeleteIndex(Index);
			_elasticClient.CreateIndex(Index);
		}

		public void Start()
		{
			Setup();
			var task = _client.GetPointsOfInterest();
            task.Wait();
			foreach (var pointOfinterest in task.Result)
			{
				_elasticClient.Index(pointOfinterest, i => i.Index(Index));
            }
		}
	}
}
