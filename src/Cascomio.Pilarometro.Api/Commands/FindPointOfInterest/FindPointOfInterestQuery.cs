
using Cascomio.Pilarometro.Common.Queries;
using Cascomio.Pilarometro.DataContract;
using Cascomio.Pilarometro.Domain;
using Nest;

namespace Cascomio.Pilarometro.Api.Commands.FindPointOfInterest
{
    public class FindPointOfInterestQuery : NestQuery<FindPointOfInterestRequest>
	{
		public override ISearchResponse<QueryResponse> Execute<TEntity>(FindPointOfInterestRequest query)
		{
			return Client.Search<PointOfInterest, QueryResponse>(s => s
					.Size(query.PageSize)
					.Index(Indexes.POINTS_OF_INTEREST)
				.Query(q => q.Match(m => m.OnField("id").Query(query.Id))));
		}
	}
}
