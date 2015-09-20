using Microsoft.Net.Http.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cascomio.Pilarometro.Import
{
	public class ServiceClient : IServiceClient
	{
		private const string Url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?types=bar|restaurant|point_of_interest&location=41.6488226,-0.8890853&key=AIzaSyDlnS1Di9cRmG8ICqGa7qD1dHfv65zdizY&radius=5000";

		public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterest()
		{
			var pointsOfInterest = new List<PointOfInterest>();
			using (var client = new HttpClient(new ManagedHandler()))
			{
				await client.GetAsync(Url)
				.ContinueWith(c =>
				{
					var json = c.Result.Content.ReadAsStringAsync();
					json.Wait();
					pointsOfInterest = JObject.Parse(json.Result).SelectToken("results").ToObject<List<PointOfInterest>>();
                });
            }
			return pointsOfInterest;
        }
	}
}
