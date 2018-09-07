using StackifyLib;

namespace BigChange.MassTransit.Stackify
{
	public class StackifyProfileTracerFactory : IStackifyProfileTracerFactory
	{
		public IProfileTracer CreateAsOperation(string operationName, string uniqueOperationId = null)
		{
			var profileTracer = ProfileTracer.CreateAsOperation(operationName, uniqueOperationId);

			return new ProfileTracerWrapper(profileTracer);
		}
	}
}