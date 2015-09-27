using Cascomio.Pilarometro.Common.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cascomio.Pilarometro.DataContract
{
    public class FindUserPointsOfInterestRequest : QueryRequest
	{
		public string Id { get; set; }
    }
}
