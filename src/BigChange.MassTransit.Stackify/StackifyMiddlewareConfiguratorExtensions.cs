using GreenPipes;
using MassTransit;

namespace BigChange.MassTransit.Stackify
{
	public static class StackifyMiddlewareConfiguratorExtensions
    {
		/// <summary>
		/// Add support for Stackify to the pipeline, which will be used to track all consumer message reception
		/// </summary>
		/// <param name="configurator"></param>
		/// <typeparam name="T"></typeparam>
		public static void UseStackify<T>(this IPipeConfigurator<T> configurator)
            where T : class, ConsumeContext
        {
	        configurator.UseStackify(new StackifyProfileTracerFactory());

        }

	    /// <summary>
	    /// Add support for Stackify to the pipeline, which will be used to track all consumer message reception
	    /// </summary>
	    /// <param name="configurator"></param>
	    /// <param name="stackifyProfileTracerFactory"></param>
	    /// <typeparam name="T"></typeparam>
		public static void UseStackify<T>(this IPipeConfigurator<T> configurator, IStackifyProfileTracerFactory stackifyProfileTracerFactory)
		    where T : class, ConsumeContext
	    {
		    configurator.AddPipeSpecification(new StackifySpecification<T>(stackifyProfileTracerFactory));
	    }
	}
}