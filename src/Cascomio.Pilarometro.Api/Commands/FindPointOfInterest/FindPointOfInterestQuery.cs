
using Cascomio.Pilarometro.Common;
using Cascomio.Pilarometro.Common.Queries;
using Cascomio.Pilarometro.DataContract;
using Cascomio.Pilarometro.Domain;
using Nest;

namespace Cascomio.Pilarometro.Api.Commands.FindPointOfInterest
{
    public class FindPointOfInterestQuery : NestQuery<FindPointOfInterestRequest>
	{
		public FindPointOfInterestQuery(IElasticClient client, ElasticsearchOptions options)
			: base(client, options){
		}

		public override ISearchResponse<PointOfInterest> Execute<PointOfInterest>(FindPointOfInterestRequest query)
		{
			return Client.Search<PointOfInterest>(s => s
					.Size(1)
					.Index(Indexes.POINTS_OF_INTEREST)
				.Query(q => q.Match(m => m.OnField("id").Query(query.Id))));
		}
	}
}
