using Cascomio.Pilarometro.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cascomio.Pilarometro.Import
{
    public interface IServiceClient
    {
		Task<IEnumerable<PointOfInterest>> GetPointsOfInterest();
    }
}
