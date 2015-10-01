using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Cascomio.Pilarometro.Common.Queries
{

	public interface IQuery<TQuery>
	{
		IEnumerable<TEntity> Query<TEntity>(TQuery query) where TEntity : class;
	}
}