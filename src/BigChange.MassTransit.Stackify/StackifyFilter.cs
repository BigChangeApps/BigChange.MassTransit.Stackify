using System.Linq;
using System.Threading.Tasks;
using GreenPipes;
using MassTransit;

namespace BigChange.MassTransit.Stackify
{
    public class StackifyFilter<T> :
        IFilter<T>
        where T : class, ConsumeContext
    {
        private const string StepName = "MassTransit:Consumer";

        private readonly IStackifyProfileTracerFactory _stackifyProfileTracerFactory;

        public StackifyFilter(IStackifyProfileTracerFactory stackifyProfileTracerFactory)
        {
            _stackifyProfileTracerFactory = stackifyProfileTracerFactory;
        }

        public void Probe(ProbeContext context)
        {
            context.CreateFilterScope("StackifyFilter");
        }

        public async Task Send(T context, IPipe<T> next)
        {
            var messageType = context.SupportedMessageTypes.FirstOrDefault() ?? "Unknown";

            var tracer =
                _stackifyProfileTracerFactory.CreateAsOperation($"{StepName} {messageType}",
                    context.MessageId?.ToString());

            await tracer.ExecAsync(async () => await next.Send(context).ConfigureAwait(false)).ConfigureAwait(false);
        }
    }
}