using System.Collections.Generic;
using System.Linq;
using GreenPipes;
using MassTransit;

namespace BigChange.MassTransit.Stackify
{
	public class StackifySpecification<T> :
        IPipeSpecification<T>
        where T : class, ConsumeContext
    {
        readonly IStackifyProfileTracerFactory _stackifyProfileTracerFactory;

        public StackifySpecification(IStackifyProfileTracerFactory stackifyProfileTracerFactory)
        {
            _stackifyProfileTracerFactory = stackifyProfileTracerFactory;
        }

        public IEnumerable<ValidationResult> Validate()
        {
            return Enumerable.Empty<ValidationResult>();
        }

        public void Apply(IPipeBuilder<T> builder)
        {
            builder.AddFilter(new StackifyFilter<T>(_stackifyProfileTracerFactory));
        }
    }
}