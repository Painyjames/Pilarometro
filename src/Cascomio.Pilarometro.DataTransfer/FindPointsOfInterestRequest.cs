using Cascomio.Pilarometro.Common.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cascomio.Pilarometro.DataContract
{
	public class FindPointsOfInterestRequest : QueryRequest
	{
		public double Latitude { get; set; }
		public double Longitude { get; set; }
	}
}
