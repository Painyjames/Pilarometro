using Cascomio.Pilarometro.Common.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;
using Cascomio.Pilarometro.Domain;
using Cascomio.Pilarometro.DataContract;

namespace Cascomio.Pilarometro.Api.Commands.FindPointsOfInterest
{
	public class FindPointsOfInterestQuery : NestQuery<FindPointsOfInterestRequest>
	{
		public override ISearchResponse<QueryResponse> Execute<TEntity>(FindPointsOfInterestRequest query)
		{
			return Client.Search<PointOfInterest, QueryResponse>(s => s
					.Size(query.PageSize)
					.Index(Indexes.POINTS_OF_INTEREST)
				.Query(q => q.Filtered(f =>
						f.Query(qd => qd.MatchAll())
						.Filter(fs => fs.GeoDistance(fd => fd.Coordinates, p =>
								 p.Distance("200m").Location(query.Latitude, query.Longitude)
							)
				))));
        }
	}
}
