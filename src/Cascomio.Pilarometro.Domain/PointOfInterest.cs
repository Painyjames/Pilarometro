using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cascomio.Pilarometro.Domain
{
    public class PointOfInterest
    {
		public string Id { get; set; }
		public string Name { get; set; }
		public double Rating { get; set; }
		public Coordinates Coordinates { get; set; }
	}

	public class Coordinates
	{
		public double? Lat { get; set; }
		public double? Lon { get; set; }
	}
}
