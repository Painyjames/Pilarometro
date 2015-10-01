using Nest;
using System.Collections.Generic;

namespace Cascomio.Pilarometro.Common.Queries
{
	public abstract class NestQuery<TQuery> : IQuery<TQuery> where TQuery : QueryRequest
	{

		protected NestQuery() { }

		public NestQuery(IElasticClient client, ElasticsearchOptions options)
		{
			Client = client;
			Options = options;
		}

		public IElasticClient Client { get; private set; }
		public ElasticsearchOptions Options { get; private set; }

		public abstract ISearchResponse<TEntity> Execute<TEntity>(TQuery query) where TEntity : class;

		public virtual IEnumerable<TEntity> Query<TEntity>(TQuery query) where TEntity : class
		{
			if (query.PageNumber <= 0)
				query.PageNumber = QueryDefaults.PageNumber;

			if (query.PageSize <= 0)
				query.PageSize = QueryDefaults.PageSize;

			return Execute<TEntity>(query).Documents;
		}
	}

}