using System;
using System.Threading;
using Contractor.Factory;
using Moq;
using Shouldly;
using Xunit;

namespace Contractor.Lifetime
{
    public class ThreadLifetimeTests
    {
        [Fact]
        public void Constructor_ShouldValidateParameters()
        {
            // Arrange
            var type = typeof(string);
            var mockFactory = new Mock<IFactory>(MockBehavior.Strict);

            // Act
            Action action1 = () => new ThreadLifetime(null, null);
            Action action2 = () => new ThreadLifetime(type, null);
            Action action3 = () => new ThreadLifetime(null, mockFactory.Object);

            // Assert
            action1.ShouldThrow<ArgumentNullException>();
            action2.ShouldThrow<ArgumentNullException>();
            action3.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void Constructor_ShouldStoreParameters()
        {
            // Arrange
            var type = typeof(string);
            var mockFactory = new Mock<IFactory>(MockBehavior.Strict);

            // Act
            var result = new ThreadLifetime(type, mockFactory.Object);

            // Assert
            result.ImplementationType.ShouldBeSameAs(type);
            result.Factory.ShouldBeSameAs(mockFactory.Object);
        }

        [Fact]
        public void Factory_ShouldThrowException_WhenValueIsNull()
        {
            // Arrange
            var type = typeof(string);
            var mockFactory = new Mock<IFactory>(MockBehavior.Strict);
            var subject = new ThreadLifetime(type, mockFactory.Object);

            // Act
            Action action = () => subject.Factory = null;

            // Assert
            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void Factory_ShouldStoreNewValue()
        {
            // Arrange
            var type = typeof(string);
            var mockFactory1 = new Mock<IFactory>(MockBehavior.Strict);
            var mockFactory2 = new Mock<IFactory>(MockBehavior.Strict);
            var subject = new ThreadLifetime(type, mockFactory1.Object);

            // Act
            subject.Factory = mockFactory2.Object;

            // Assert
            subject.Factory.ShouldBeSameAs(mockFactory2.Object);
        }

        [Fact]
        public void GetInstance_ShouldConstructSingleInstancePerThread()
        {
            // Arrange
            var type = typeof(string);
            var instances = new[] { "hello", "goodbye" };
            var count1 = 0;
            var count3 = 1;

            var mockFactory1 = new Mock<IFactory>(MockBehavior.Strict);
            mockFactory1
                .Setup(f => f.ConstructNewInstance())
                .Callback(() => Thread.Sleep(1000))
                .Returns(instances[count1++]);

            var mockFactory2 = new Mock<IFactory>(MockBehavior.Strict);

            var mockFactory3 = new Mock<IFactory>(MockBehavior.Strict);
            mockFactory3
                .Setup(f => f.ConstructNewInstance())
                .Callback(() => Thread.Sleep(1000))
                .Returns(instances[count3--]);

            var subject1 = new ThreadLifetime(type, mockFactory1.Object);
            var subject2 = new ThreadLifetime(type, mockFactory2.Object);
            var subject3 = new ThreadLifetime(type, mockFactory3.Object);

            object result1 = null;
            object result2 = null;
            object result3 = null;
            object result4 = null;
            object result5 = null;
            object result6 = null;
            object result7 = null;
            object result8 = null;
            object result9 = null;

            var thread1_2 = new Thread(() =>
            {
                result1 = subject1.GetInstance();
                result2 = subject1.GetInstance();
                result3 = subject1.GetInstance();

                result4 = subject2.GetInstance();
                result5 = subject2.GetInstance();
                result6 = subject2.GetInstance();
            });

            var thread3 = new Thread(() =>
            {
                result7 = subject3.GetInstance();
                result8 = subject3.GetInstance();
                result9 = subject3.GetInstance();
            });

            // Act
            thread1_2.Start();
            thread3.Start();

            thread1_2.Join();
            thread3.Join();

            // Assert
            result1.ShouldBe(instances[0]);
            result2.ShouldBe(instances[0]);
            result3.ShouldBe(instances[0]);

            result4.ShouldBe(instances[0]);
            result5.ShouldBe(instances[0]);
            result6.ShouldBe(instances[0]);

            result7.ShouldBe(instances[1]);
            result8.ShouldBe(instances[1]);
            result9.ShouldBe(instances[1]);

            mockFactory1.VerifyAll();
            mockFactory3.VerifyAll();
        }
    }
}
