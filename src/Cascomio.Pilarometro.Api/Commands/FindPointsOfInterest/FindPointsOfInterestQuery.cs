using Cascomio.Pilarometro.Common.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;
using Cascomio.Pilarometro.Domain;
using Cascomio.Pilarometro.DataContract;
using Cascomio.Pilarometro.Common;

namespace Cascomio.Pilarometro.Api.Commands.FindPointsOfInterest
{
	public class FindPointsOfInterestQuery : NestQuery<FindPointsOfInterestRequest>
	{
		public FindPointsOfInterestQuery(IElasticClient client, ElasticsearchOptions options)
			: base(client, options){
		}

		public override ISearchResponse<PointOfInterest> Execute<PointOfInterest>(FindPointsOfInterestRequest query)
		{
			return Client.Search<PointOfInterest>(s => s
					.Size(query.PageSize)
					.Index(Indexes.POINTS_OF_INTEREST)
				.Query(q => q.Filtered(f =>
						f.Query(qd => qd.MatchAll())
						.Filter(fs => fs.GeoDistance("coordinates", p =>
								 p.Distance("500m").Location(query.Latitude, query.Longitude)
							)
				))));
        }
	}
}
