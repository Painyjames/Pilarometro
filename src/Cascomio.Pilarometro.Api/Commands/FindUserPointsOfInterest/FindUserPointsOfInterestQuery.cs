using Cascomio.Pilarometro.Common.Queries;
using Cascomio.Pilarometro.DataContract;
using Cascomio.Pilarometro.Domain;
using Nest;

namespace Cascomio.Pilarometro.Api.Commands.FindUserPointsOfInterest
{
    public class FindUserPointsOfInterestQuery : NestQuery<FindUserPointsOfInterestRequest>
	{
		public override ISearchResponse<QueryResponse> Execute<TEntity>(FindUserPointsOfInterestRequest query)
		{
			return Client.Search<PointOfInterest, QueryResponse>(s => s
				.Size(query.PageSize)
				.Index(Indexes.PILAROMETRO)
			.Query(q => q.Match(m => m.OnField("id").Query(query.Id))));
		}
    }
}
