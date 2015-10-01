using Cascomio.Pilarometro.Common;
using Cascomio.Pilarometro.Common.Queries;
using Cascomio.Pilarometro.DataContract;
using Cascomio.Pilarometro.Domain;
using Nest;

namespace Cascomio.Pilarometro.Api.Commands.FindUserPointsOfInterest
{
    public class FindUserPointsOfInterestQuery : NestQuery<FindUserPointsOfInterestRequest>
	{
		public FindUserPointsOfInterestQuery(IElasticClient client, ElasticsearchOptions options)
			: base(client, options){
		}

        public override ISearchResponse<PointOfInterest> Execute<PointOfInterest>(FindUserPointsOfInterestRequest query)
		{
			return Client.Search<PointOfInterest>(s => s
				.Size(query.PageSize)
				.Index(Indexes.PILAROMETRO)
			.Query(q => q.Match(m => m.OnField("id").Query(query.Id))));
		}
    }
}
