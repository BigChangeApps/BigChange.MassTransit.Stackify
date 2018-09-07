namespace BigChange.MassTransit.Stackify
{
	public interface IStackifyProfileTracerFactory
	{
		IProfileTracer CreateAsOperation(string operationName, string uniqueOperationId = null);
	}
}