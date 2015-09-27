﻿using Cascomio.Pilarometro.Common.Commands;
using Cascomio.Pilarometro.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using AutoMapper;
using Nest;
using Cascomio.Pilarometro.Domain;
using Cascomio.Pilarometro.Common.Queries;

namespace Cascomio.Pilarometro.Api.Commands.UpdateUserPointsOfInterest
{
	public class UpdateUserPointsOfInterestCommand : Command<UpdateUserPointsOfInterestRequest, UpdateUserPointsOfInterestResponse>
	{

		private readonly IMappingEngine _mappingEngine;
		private readonly IElasticClient _elasticClient;

		public UpdateUserPointsOfInterestCommand(IMappingEngine mappingEngine,
												 IElasticClient elasticClient)
            : base("point-of-interest", "update-user-points-of-interest", TimeSpan.FromMilliseconds(15000))
        {
			_mappingEngine = mappingEngine;
			_elasticClient = elasticClient;
        }

		protected override Task<UpdateUserPointsOfInterestResponse> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task<UpdateUserPointsOfInterestResponse>.Factory.StartNew(() =>
			{
				cancellationToken.ThrowIfCancellationRequested();

				var user = _mappingEngine.Map<User>(Request.User);
                var response = _elasticClient.Update<User>(u => u.Index(Indexes.PILAROMETRO).Id(user, useAsUpsert: true));

				return new UpdateUserPointsOfInterestResponse
				{
					User = _mappingEngine.Map<UserDto>(_elasticClient.Get<User>(user.Id).Source)
				};
			});
		}
	}
}
