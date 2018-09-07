using System;
using System.Threading;
using System.Threading.Tasks;

namespace BigChange.MassTransit.Stackify.Tests
{
	public class TestProfileTracer : IProfileTracer
	{
		private int _count;
		public int Count => _count;
		
		public Task ExecAsync(Func<Task> task)
		{
			Interlocked.Increment(ref _count);

			return task.Invoke();
		}
	}
}