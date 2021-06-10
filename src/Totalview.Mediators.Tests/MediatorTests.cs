using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Totalview.Mediators
{
    public class MediatorTests
    {
        private Mediator GetUnit()
        {
            return new Mediator();
        }

        /* ----------------------------------------------------------------------------  */
        /*                       Subscribe / SendNotificationAsync                       */
        /* ----------------------------------------------------------------------------  */
        [Fact]
        public async Task SendNotificationAsync_Should_handle_no_subscriptions()
        {
            //Arrange
            Mediator unit = GetUnit();

            //Act
            await unit.SendNotificationAsync(new TestNotification());

            //Assert
        }

        [Fact]
        public async Task SendNotificationAsync_Should_send_notification_to_single_subscription()
        {
            //Arrange
            Mediator unit = GetUnit();

            bool subscription1Called = false;
            ISubscription subscription1 = unit.Subscribe((TestNotification n, CancellationToken ct) =>
            {
                subscription1Called = true;
                return Task.CompletedTask;
            });

            //Act
            await unit.SendNotificationAsync(new TestNotification());

            //Assert
            Assert.True(subscription1Called);
        }

        [Fact]
        public async Task SendNotificationAsync_Should_send_notification_to_multiple_subscriptions()
        {
            //Arrange
            Mediator unit = GetUnit();

            bool subscription1Called = false;
            ISubscription subscription1 = unit.Subscribe((TestNotification n, CancellationToken ct) =>
            {
                subscription1Called = true;
                return Task.CompletedTask;
            });

            bool subscription2Called = false;
            ISubscription subscription2 = unit.Subscribe((TestNotification n, CancellationToken ct) =>
            {
                subscription2Called = true;
                return Task.CompletedTask;
            });

            //Act
            await unit.SendNotificationAsync(new TestNotification());

            //Assert
            Assert.True(subscription1Called);
            Assert.True(subscription2Called);
        }

        [Fact]
        public async Task SendNotificationAsync_Should_not_send_notification_to_disposed_subscriptions()
        {
            //Arrange
            Mediator unit = GetUnit();

            bool subscription1Called = false;
            ISubscription subscription1 = unit.Subscribe((TestNotification n, CancellationToken ct) =>
            {
                subscription1Called = true;
                return Task.CompletedTask;
            });


            bool subscription2Called = false;
            ISubscription subscription2 = unit.Subscribe((TestNotification n, CancellationToken ct) =>
            {
                subscription2Called = true;
                return Task.CompletedTask;
            });

            subscription1.Dispose();

            //Act
            await unit.SendNotificationAsync(new TestNotification());

            //Assert
            Assert.False(subscription1Called);
            Assert.True(subscription2Called);
        }

        [Fact]
        public async Task SendNotificationAsync_Should_handle_disposing_of_all_subscriptions()
        {
            //Arrange
            Mediator unit = GetUnit();

            bool subscription1Called = false;
            ISubscription subscription1 = unit.Subscribe((TestNotification n, CancellationToken ct) =>
            {
                subscription1Called = true;
                return Task.CompletedTask;
            });


            bool subscription2Called = false;
            ISubscription subscription2 = unit.Subscribe((TestNotification n, CancellationToken ct) =>
            {
                subscription2Called = true;
                return Task.CompletedTask;
            });

            subscription1.Dispose();
            subscription2.Dispose();

            //Act
            await unit.SendNotificationAsync(new TestNotification());

            //Assert
            Assert.False(subscription1Called);
            Assert.False(subscription2Called);
        }

        /* ----------------------------------------------------------------------------  */
        /*                                  SOMECOMMENT                                  */
        /* ----------------------------------------------------------------------------  */
        [Fact]
        public async Task RequestAsync_Should_throw_exception_if_request_was_not_handled()
        {
            //Arrange
            Mediator unit = GetUnit();

            //Act
            Exception exception = await Record.ExceptionAsync(() => unit.RequestAsync(new TestRequest()));

            //Assert
            Assert.IsType<RequestNotHandledException>(exception);
        }


        [Fact]
        public async Task RequestAsync_Should_handle_request()
        {
            //Arrange
            Mediator unit = GetUnit();

            string expectedResult = "Test";
            ISubscription subscription1 = unit.Subscribe((TestRequest testRequest, CancellationToken ct) => Task.FromResult(expectedResult));

            //Act
            string result = await unit.RequestAsync(new TestRequest());

            //Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void RequestAsync_Should_throw_exception_on_multiple_subscriptions()
        {
            //Arrange
            Mediator unit = GetUnit();

            //Act
            ISubscription subscription1 = unit.Subscribe((TestRequest testRequest, CancellationToken ct) => Task.FromResult("Test"));
            Exception exception = Record.Exception(() => unit.Subscribe((TestRequest testRequest, CancellationToken ct) => Task.FromResult("Test2")));

            //Assert
            Assert.IsType<RequestAlreadySubscribedException>(exception);
        }

        [Fact]
        public async Task RequestAsync_Should_handle_dispose_of_subscription()
        {
            //Arrange
            Mediator unit = GetUnit();

            ISubscription subscription1 = unit.Subscribe((TestRequest testRequest, CancellationToken ct) => Task.FromResult("Test"));

            subscription1.Dispose();

            //Act
            Exception exception = await Record.ExceptionAsync(() => unit.RequestAsync(new TestRequest()));

            //Assert
            Assert.IsType<RequestNotHandledException>(exception);
        }

        [Fact]
        public async Task RequestAsync_Should_handle_dispose_of_subscription_and_readding()
        {
            //Arrange
            Mediator unit = GetUnit();

            string expectedResult = "Test2";
            ISubscription subscription1 = unit.Subscribe((TestRequest testRequest, CancellationToken ct) => Task.FromResult("Test"));

            subscription1.Dispose();

            ISubscription subscription2 = unit.Subscribe((TestRequest testRequest, CancellationToken ct) => Task.FromResult(expectedResult));

            //Act
            string result = await unit.RequestAsync(new TestRequest());

            //Assert
            Assert.Equal(expectedResult, result);
        }


        /* ----------------------------------------------------------------------------  */
        /*                               INTERNAL CLASSES                                */
        /* ----------------------------------------------------------------------------  */
        private class TestNotification : INotification { }

        private class TestRequest : IRequest<string> { }
    }
}
