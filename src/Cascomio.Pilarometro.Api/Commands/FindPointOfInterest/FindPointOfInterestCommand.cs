using AutoMapper;
using Cascomio.Pilarometro.Common.Commands;
using Cascomio.Pilarometro.Common.Queries;
using Cascomio.Pilarometro.DataContract;
using Cascomio.Pilarometro.Domain;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cascomio.Pilarometro.Api.Commands
{
    public class FindPointOfInterestCommand : Command<FindPointOfInterestRequest, FindPointOfInterestResponse>
	{

		private readonly NestQuery<FindPointOfInterestRequest> _findPointOfInterestQuery;
		private readonly IMappingEngine _mappingEngine;

		public FindPointOfInterestCommand(NestQuery<FindPointOfInterestRequest> findPointOfInterestQuery,
										  IMappingEngine mappingEngine)
            : base("point-of-interest", "find-point-of-interest", TimeSpan.FromMilliseconds(15000))
        {
			_findPointOfInterestQuery = findPointOfInterestQuery;
			_mappingEngine = mappingEngine;
        }

		protected override Task<FindPointOfInterestResponse> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task<FindPointOfInterestResponse>.Factory.StartNew(() =>
			{
				cancellationToken.ThrowIfCancellationRequested();

				var result = _findPointOfInterestQuery.Execute<PointOfInterest>(Request);

				return new FindPointOfInterestResponse
				{					
					PointOfInterest = _mappingEngine.Map<PointOfInterestDto>(result.Documents.First())
				};
			});
		}
	}
}
