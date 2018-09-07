using System;
using System.Threading.Tasks;
using StackifyLib;

namespace BigChange.MassTransit.Stackify
{
	public class ProfileTracerWrapper : IProfileTracer
	{
		private readonly ProfileTracer _profileTracer;

		public ProfileTracerWrapper(ProfileTracer profileTracer)
		{
			_profileTracer = profileTracer;
		}

		public Task ExecAsync(Func<Task> task)
		{
			return _profileTracer.ExecAsync(task);
		}
	}
}