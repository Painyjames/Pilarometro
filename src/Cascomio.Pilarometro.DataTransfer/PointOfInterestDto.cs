using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cascomio.Pilarometro.DataContract
{
    public class PointOfInterestDto
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public double Rating { get; set; }
		public CoordinatesDto Coordinates { get; set; }
	}

	public class CoordinatesDto
	{
		public double? Lat { get; set; }
		public double? Lon { get; set; }
	}
}
