using System;
using System.Collections.Generic;
using Elasticsearch.Net.ConnectionPool;
using Nest;

namespace Cascomio.Pilarometro.Common
{
	public class ElasticsearchOptions
	{
		public List<Uri> Url { get; set; }

		public bool EnableTrace { get; set; }

		public string Index { get; set; }

		public string Type { get; set; }

		public ConnectionSettings Get()
		{
			var pool = new SniffingConnectionPool(Url);

			var settings = new ConnectionSettings(pool);
			settings.ThrowOnElasticsearchServerExceptions();

			if (EnableTrace)
				settings.EnableTrace();

			return settings;
		}
	}
}