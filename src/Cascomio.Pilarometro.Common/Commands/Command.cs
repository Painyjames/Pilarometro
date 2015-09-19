using System;
using Hudl.Mjolnir.Command;
using System.Threading.Tasks;

namespace Cascomio.Pilarometro.Common.Commands
{
	public abstract class Command<TResquest, TResponse> : Command<TResponse>, ICommand<TResquest, TResponse>
	{

		public Command(string group, string isolationKey, TimeSpan defaultTimeout)
			: base(group, isolationKey, defaultTimeout)
		{
		}

		public Command(string group, string isolationKey)
			: base(group, isolationKey, TimeSpan.FromMilliseconds(15000))
		{

		}

		public TResquest Request { get; private set; }

		public Task<TResponse> Execute(TResquest request)
		{
			Request = request;
			return InvokeAsync();
		}
	}
}