using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cascomio.Pilarometro.Import
{
    public class PointOfInterest
    {
		public string Name { get; set; }
		public double Rating { get; set; }
		public Geometry Geometry { get; set; }
	}

	public class Geometry
	{
		public Location Location { get; set; }
	}

	public class Location
	{
		public double Lat { get; set; }
		public double Lng { get; set; }
	}

}
