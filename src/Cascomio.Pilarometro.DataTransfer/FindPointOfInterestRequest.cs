using Cascomio.Pilarometro.Common.Queries;

namespace Cascomio.Pilarometro.DataContract
{
	public class FindPointOfInterestRequest : QueryRequest
	{
		public string Id { get; set; }
	}
}
