using Cascomio.Pilarometro.Domain;
using Microsoft.Net.Http.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cascomio.Pilarometro.Import
{
	public class ServiceClient : IServiceClient
	{

		private const double Top = 41.710261;
		private const double Bottom = 41.607994;
		private const double Left = -0.975094;
		private const double Right = -0.781354;
		private const double Precision = 0.01;
		private const string Url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?types=bar|restaurant|point_of_interest&location={0},{1}&key=AIzaSyCeT-vkL-U_0L0LaMJicDwXu_5-G0ZiyfA&radius=500";

		public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterest()
		{
			var tasks = new List<Task>();
			var pointsOfInterest = new List<PointOfInterest>();
			using (var client = new HttpClient(new ManagedHandler()))
			{

				double actualLatitude = Top;
				while (actualLatitude > Bottom)
				{
					double actualLongitude = Left;
					while (actualLongitude < Right)
					{
						tasks.Add(client.GetAsync(string.Format(Url, actualLatitude, actualLongitude))
						.ContinueWith(c =>
						{
							var json = c.Result.Content.ReadAsStringAsync();
							json.Wait();
							pointsOfInterest.AddRange(JObject.Parse(json.Result).SelectToken("results")
											   .ToObject<List<PointOfInterestImport>>()
											   .Select(s => new PointOfInterest
											   {
												   Id = s.Id,
												   Name = s.Name,
												   Rating = s.Rating,
												   Coordinates = new Coordinates
												   {
													   Lat = s.Geometry?.Location?.Lat,
													   Lon = s.Geometry?.Location?.Lng
												   }
											   }));
						}));
						actualLongitude = actualLongitude + Precision;
					}

					actualLatitude = actualLatitude - Precision;
				}
				Task.WaitAll(tasks.ToArray());
            }
			return pointsOfInterest;
        }
	}
}
