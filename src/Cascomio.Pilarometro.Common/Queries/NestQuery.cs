using Nest;
using System.Collections.Generic;

namespace Cascomio.Pilarometro.Common.Queries
{
	public abstract class NestQuery<TQuery> : IQuery<TQuery> where TQuery : QueryRequest
	{

		protected NestQuery() { }

		public NestQuery(IElasticClient client, IRepository repository, ElasticsearchOptions options)
		{
			Client = client;
			Options = options;
		}

		public IElasticClient Client { get; private set; }
		public ElasticsearchOptions Options { get; private set; }

		public abstract ISearchResponse<QueryResponse> Execute<TEntity>(TQuery query);

		public virtual IEnumerable<QueryResponse> Query<TEntity>(TQuery query)
		{
			if (query.PageNumber <= 0)
				query.PageNumber = QueryDefaults.PageNumber;

			if (query.PageSize <= 0)
				query.PageSize = QueryDefaults.PageSize;

			var response = Execute<TEntity>(query);
			
			return response.Documents;
		}
	}

}