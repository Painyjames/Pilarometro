using System;
using Hudl.Mjolnir.Command;
using System.Threading.Tasks;

namespace Cascomio.Pilarometro.Common.Commands
{
	public interface ICommand<TResquest, TResponse> : ICommand<TResponse>
	{
		Task<TResponse> Execute(TResquest request);
	}
}