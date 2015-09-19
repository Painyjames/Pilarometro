using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Cascomio.Pilarometro.Common.Queries
{

	public interface IQuery<TQuery>
	{
		IEnumerable<QueryResponse> Query<TEntity>(TQuery query);
	}
}