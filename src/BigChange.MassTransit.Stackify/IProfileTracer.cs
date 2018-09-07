using System;
using System.Threading.Tasks;

namespace BigChange.MassTransit.Stackify
{
    public interface IProfileTracer
    {
        Task ExecAsync(Func<Task> task);
    }
}