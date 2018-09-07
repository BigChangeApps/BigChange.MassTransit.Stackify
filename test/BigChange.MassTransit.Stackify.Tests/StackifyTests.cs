using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Testing;
using Xunit;
using Moq;
using Shouldly;

namespace BigChange.MassTransit.Stackify.Tests
{
    public class StackifyTests : IAsyncLifetime
    {
        private InMemoryTestHarness _harness;
        private ConsumerTestHarness<Testsumer> _consumer;
        private TestProfileTracer _profileTracer;

        public async Task InitializeAsync()
        {
            _harness = new InMemoryTestHarness();

            _profileTracer = new TestProfileTracer();

            var stackifyProfileTracerFactory = new Mock<IStackifyProfileTracerFactory>();
            stackifyProfileTracerFactory.Setup(x =>
                    x.CreateAsOperation("MassTransit:Consumer urn:message:BigChange.MassTransit.Stackify.Tests:StackifyTests+A",
                        It.IsAny<string>()))
                .Returns(_profileTracer);

            _harness.OnConfigureInMemoryBus += configurator =>
            {
                configurator.UseStackify(stackifyProfileTracerFactory.Object);
            };

            _consumer = _harness.Consumer<Testsumer>();

            await _harness.Start().ConfigureAwait(false);

            await _harness.InputQueueSendEndpoint.Send(new A()).ConfigureAwait(false);
        }


        [Fact]
        public void ShouldConsumeAndBeTraced()
        {
            _consumer.Consumed.Select<A>().Any().ShouldBe(true);
            _harness.Consumed.Select<A>().Any().ShouldBe(true);

            _profileTracer.Count.ShouldBe(1);
        }

        public async Task DisposeAsync()
        {
            await _harness.Stop().ConfigureAwait(false);
        }


        private class Testsumer :
            IConsumer<A>
        {
            public Task Consume(ConsumeContext<A> context)
            {
                return Task.CompletedTask;
            }
        }

        private class A
        {
        }
    }
}

