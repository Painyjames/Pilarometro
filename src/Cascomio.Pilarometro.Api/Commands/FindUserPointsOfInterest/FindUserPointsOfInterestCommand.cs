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

namespace Cascomio.Pilarometro.Api.Commands.FindUserPointsOfInterest
{
    public class FindUserPointsOfInterest : Command<FindUserPointsOfInterestRequest, FindUserPointsOfInterestResponse>
	{

		private readonly NestQuery<FindUserPointsOfInterestRequest> _findPointOfInterestQuery;
		private readonly IMappingEngine _mappingEngine;

		public FindUserPointsOfInterest(NestQuery<FindUserPointsOfInterestRequest> findPointOfInterestQuery,
										IMappingEngine mappingEngine)
            : base("point-of-interest", "find-user-points-of-interest", TimeSpan.FromMilliseconds(15000))
        {
			_findPointOfInterestQuery = findPointOfInterestQuery;
			_mappingEngine = mappingEngine;
		}

		protected override Task<FindUserPointsOfInterestResponse> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task<FindUserPointsOfInterestResponse>.Factory.StartNew(() =>
			{
				cancellationToken.ThrowIfCancellationRequested();

				var result = _findPointOfInterestQuery.Execute<User>(Request);

				return new FindUserPointsOfInterestResponse
				{
					User = _mappingEngine.Map<UserDto>(result.Documents.First())
				};
			});
		}
	}
}
