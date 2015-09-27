using AutoMapper;
using Cascomio.Pilarometro.Common.Commands;
using Cascomio.Pilarometro.Common.Queries;
using Cascomio.Pilarometro.DataContract;
using Cascomio.Pilarometro.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cascomio.Pilarometro.Api.Commands
{
    public class FindPointsOfInterestCommand : Command<FindPointsOfInterestRequest, FindPointsOfInterestResponse>
	{

		private readonly NestQuery<FindPointsOfInterestRequest> _findPointsOfInterestQuery;
		private readonly IMappingEngine _mappingEngine;

		public FindPointsOfInterestCommand(NestQuery<FindPointsOfInterestRequest> findPointsOfInterestQuery,
										   IMappingEngine mappingEngine)
            : base("point-of-interest", "find-points-of-interest", TimeSpan.FromMilliseconds(15000))
        {
			_findPointsOfInterestQuery = findPointsOfInterestQuery;
			_mappingEngine = mappingEngine;
        }

		protected override Task<FindPointsOfInterestResponse> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task<FindPointsOfInterestResponse>.Factory.StartNew(() =>
			{
				cancellationToken.ThrowIfCancellationRequested();

				var result = _findPointsOfInterestQuery.Execute<PointOfInterest>(Request);

				return new FindPointsOfInterestResponse
				{
					PointsOfInterest = _mappingEngine.Map<List<PointOfInterestDto>>(result.Documents)
				};
			});
		}
	}
}
